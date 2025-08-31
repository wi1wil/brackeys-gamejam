using System.Collections;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyChaseScript : MonoBehaviour
{
    public float speed = 2f;
    public float lineOfSightRange = 10f;
    public float proximityRange = 10f;
    public float patrolRange = 10f;
    public float attackRange = 2.5f;
    public int count = 0;
    public int total = 0;
    public int attackCooldown = 5;

    public bool isLOS = false;
    public bool isProximity = false;
    public bool isPatrol = false;
    public bool isPatrolling = false;
    public bool isDroppingPoison = false;
    public bool isAttacking = false;
    public bool isWalking = false;

    public bool reachedMax = false;
    public bool reachedMin = false;

    public Transform target;
    public GameObject ratPoison;
    public GameObject poisonParent;
    public GameObject patrolPoints;
    public Transform[] points;
    public Vector2 spawnPos;

    PlayerHealthScript playerHealthScript;
    PlayerInputScript playerInputScript;
    StagesScript stagesScript;
    SpriteRenderer spriteRenderer;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
        stagesScript = FindAnyObjectByType<StagesScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        poisonParent = GameObject.Find("PoisonParent");
        spawnPos = transform.position;
    }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;

        if (gameObject.CompareTag("Patrol"))
        {
            isPatrolling = true;
            updatePoints();
            agent.SetDestination(points[0].position);
        }
    }

    void Update()
    {
        if (gameObject.CompareTag("LOS"))
        {
            enemyLOS();
            isLOS = true;
        }

        if (gameObject.CompareTag("Proximity"))
        {
            enemyProximity();
            isProximity = true;
        }

        if (gameObject.CompareTag("Patrol"))
        {
            updatePoints();
            enemyPatrol();
            isPatrol = true;
        }
        Vector2 velocity = agent.velocity;

        if (velocity.magnitude > 0.1f)
        {
            animator.SetFloat("InputX", Mathf.Round(velocity.normalized.x));
            animator.SetFloat("InputY", Mathf.Round(velocity.normalized.y));
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void updatePoints()
    {
        int totalPoints = patrolPoints.transform.childCount;
        if (total == totalPoints) return;

        total = totalPoints;
        points = new Transform[total];

        for (int i = 0; i < total; i++)
        {
            points[i] = patrolPoints.transform.GetChild(i);
        }
    }

    void OnDrawGizmos()
    {
        if (isLOS)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, lineOfSightRange);
        }

        if (isProximity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, proximityRange);
        }

        if (isPatrol)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, patrolRange);
        }

        if (target != null)
        {
            Vector3 origin = transform.position;
            bool clear = LineOfSight(target);

            Gizmos.color = clear ? Color.green : Color.yellow;
            Gizmos.DrawLine(origin, target.position);
        }
    }

    void enemyLOS()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, lineOfSightRange, LayerMask.GetMask("Player"));
        if (hits.Length > 0)
        {
            PlayerInputScript player = hits[0].GetComponent<PlayerInputScript>();
            if (player != null)
            {
                if (LineOfSight(player.transform))
                {
                    target = player.transform;
                    UpdateBehaviour();
                    return;
                }
            }
        }
        else
        {
            target = null;
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            return;
        }
    }

    void enemyProximity()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, proximityRange, LayerMask.GetMask("Player"));
        if (hits.Length > 0)
        {
            PlayerInputScript player = hits[0].GetComponent<PlayerInputScript>();
            if (player != null)
            {
                target = player.transform;
                UpdateBehaviour();
                return;
            }
        }
        else
        {
            target = null;
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            return;
        }
    }

    void enemyPatrol()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, patrolRange, LayerMask.GetMask("Player"));
        if (hits.Length > 0)
        {
            PlayerInputScript player = hits[0].GetComponent<PlayerInputScript>();
            if (player != null && stagesScript.currentStageLevel > 2)
            {
                isPatrolling = false;
                target = player.transform;
                UpdateBehaviour();
                return;
            }
        }
        else
        {
            isPatrolling = true;
            target = null;
        }

        // Patrol movement
        if (points.Length > 0 && isPatrolling)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                if (count == 0)
                {
                    reachedMax = false;
                    reachedMin = true;
                    count++;
                }
                else if (count == points.Length - 1)
                {
                    reachedMax = true;
                    count--;
                }
                else if (reachedMax == true)
                {
                    count--;
                }
                else if (reachedMax == false && reachedMin == true)
                {
                    count++;
                }
                agent.SetDestination(points[count].position);
            }
        }


        // While patrolling drop rat poison if currentStage = 2;
        if (stagesScript.currentStageLevel == 2 && !isDroppingPoison)
        {
            StartCoroutine(droppingPoison());
        }
    }

    public bool LineOfSight(Transform target)
    {
        LayerMask obstacleLayer = LayerMask.GetMask("Obstacle");
        Vector2 origin = transform.position;
        Vector2 direction = (Vector2)target.position - origin;
        float distance = Vector2.Distance(origin, target.position);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, distance, obstacleLayer);
        return hit.collider == null;
    }

    void UpdateBehaviour()
    {
        if (target == null) return;

        float distanceToPlayer = Vector2.Distance(target.position, transform.position);

        // Attack if in range and not already attacking
        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            isAttacking = true;
            agent.isStopped = true;
            StartCoroutine(AttackAndRetreat());
        }
        // Chase if player is out of attack range
        else if (distanceToPlayer > attackRange && !isAttacking)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }

    IEnumerator AttackAndRetreat()
    {
        // Attack
        Debug.Log("Attacking player");
        playerHealthScript.TakeDamage();

        // Short pause to simulate attack animation before retreat
        yield return new WaitForSeconds(0.5f);

        // Retreat (go back to patrol point or spawn)
        if (isPatrol && points.Length > 0)
        {
            agent.SetDestination(points[count++].position);
        }
        else
        {
            agent.SetDestination(spawnPos);
        }
        agent.isStopped = false;

        // Cooldown before enemy can attack again
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    IEnumerator droppingPoison()
    {
        isDroppingPoison = true;
        GameObject poison = Instantiate(ratPoison, transform.position, Quaternion.identity, poisonParent.transform);
        yield return new WaitForSeconds(5f);
        Destroy(poison, 15f);
        isDroppingPoison = false;
    }

    public void callGetPoisoned()
    {
        StartCoroutine(gotPoisoned());
    }

    IEnumerator gotPoisoned()
    {
        Debug.Log("I got hit with poison");
        playerInputScript.speed--;
        yield return new WaitForSeconds(5f);
        playerInputScript.speed++;
        Debug.Log("Adding back speed");
    }
}

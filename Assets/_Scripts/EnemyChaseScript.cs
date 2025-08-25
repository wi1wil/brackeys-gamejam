using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using System.Xml.Schema;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyChaseScript : MonoBehaviour
{
    public float speed = 2f;
    public float lineOfSightRange = 5f;
    public float proximityRange = 10f;
    public float patrolRange = 7.5f;
    public int count = 0;
    public int total = 0;

    public bool isLOS = false;
    public bool isProximity = false;
    public bool isPatrol = false;


    public bool reachedMax = false;
    public bool reachedMin = false;

    public Transform target;
    public GameObject patrolPoints;
    public Transform[] points;

    SpriteRenderer spriteRenderer;
    NavMeshAgent agent;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (gameObject.CompareTag("Patrol"))
        {
            patrolPoints = GameObject.Find("PatrolPoints");
            isPatrol = true;
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
        foreach (Collider2D hit in hits)
        {
            PlayerInputScript player = hit.GetComponent<PlayerInputScript>();
            if (player != null)
            {
                if (LineOfSight(player.transform))
                {
                    target = player.transform;
                    UpdateBehaviour();
                    break;
                }
                else
                {
                    target = null;
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    break;
                }
            }
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
                Debug.Log("Proximity");
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
            if (player != null)
            {
                isPatrol = false;
                target = player.transform;
                UpdateBehaviour();
                return;
            }
        }
        else
        {
            isPatrol = true;
            target = null;
        }
        
        // Patrol movement
        if (points.Length > 0 && isPatrol)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                Debug.Log($"Patrolled at {count}");
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
        if (target != null)
        {
            agent.isStopped = false;
            agent.speed = speed;
            agent.SetDestination(target.position);
        }
    }
}

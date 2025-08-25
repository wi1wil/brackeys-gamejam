using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyChaseScript : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 15f;

    public Transform target;

    SpriteRenderer spriteRenderer;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (gameObject.CompareTag("LOS"))
        {
            chasePlayers();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (target != null)
        {
            Vector3 origin = transform.position;
            bool clear = LineOfSight(target);

            Gizmos.color = clear ? Color.green : Color.yellow;
            Gizmos.DrawLine(origin, target.position);
        }
    }

    void chasePlayers()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, LayerMask.GetMask("Player"));
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
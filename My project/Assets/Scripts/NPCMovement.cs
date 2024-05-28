using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    private int destPoint = 0;
    private Rigidbody2D rb;
    private bool waiting = false;
    private Vector2 avoidanceDirection = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        GotoNextPoint();
    }

    public void SetAvoidanceDirection(Vector2 direction)
    {
        avoidanceDirection = direction;
    }

    public void StartPatrolling()
    {
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        destPoint = Random.Range(0, patrolPoints.Length);

        StopAllCoroutines();
        StartCoroutine(MoveToNextPoint());
    }

    IEnumerator MoveToNextPoint()
    {
        while (Vector2.Distance(transform.position, patrolPoints[destPoint].position) > 0.2f)
        {
            Vector2 direction = (patrolPoints[destPoint].position - transform.position).normalized;

            direction += avoidanceDirection;

            rb.velocity = direction * speed;

            yield return null;
        }

        rb.velocity = Vector2.zero;
        StartCoroutine(WaitBeforeNextPoint());
    }

    void Update()
    {
        if (!waiting && Vector2.Distance(transform.position, patrolPoints[destPoint].position) < 0.2f)
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(WaitBeforeNextPoint());
        }
    }

    IEnumerator WaitBeforeNextPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(2);
        waiting = false;
        GotoNextPoint();
    }
}

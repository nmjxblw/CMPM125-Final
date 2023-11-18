using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviours : MonoBehaviour
{
    public float attackRange = 0.1f;
    public float currentSpeed = 4f;
    public GameObject target;
    public LayerMask playerLayer = new LayerMask();
    private EnemyState currentState = EnemyState.Idle;
    public EnemyState CurrentState
    {
        get
        { return this.currentState; }
        set
        { this.currentState = value; }
    }
    private float innerClock = 0f;
    private Rigidbody2D targetRb;
    private Vector2 destination;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private float distance;


    private void Awake()
    {
        target = target ? target : GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        targetRb = target.GetComponent<Rigidbody2D>();
        destination = rb.position;
    }


    private bool isTargetInSight()
    {

        Vector2 rayOrigin = new Vector2(transform.position.x + 0.5f, transform.position.y + 1.5f);
        float viewAngle = 90f;
        float viewRadius = 10f;

        for (int i = 0; i < 180; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + i * (viewAngle / 180f);
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, viewRadius, playerLayer);
            Debug.DrawRay(rayOrigin, rayDirection * viewRadius, Color.red);
            if (hit.collider != null && hit.collider.gameObject == target)
            {

                return true;
            }
        }
        return false;
    }

    public virtual void Update()
    {
        innerClock = Time.deltaTime;
        HandleState();
        distance = Vector2.Distance(rb.position, targetRb.position);
        if (isTargetInSight() && distance > attackRange)
        {
            destination = targetRb.position;
        }
        else
        {
            destination = rb.position;
        }
        moveDirection = (destination - rb.position).normalized;
    }

    public void FixedUpdate()
    {
        MoveToDestination();
    }

    public void MoveToDestination()
    {
        rb.velocity = new Vector2(currentSpeed * moveDirection.x, rb.velocity.y);
    }

    public void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Patrol:
                HandlePatrol();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
            default:
                break;
        }
    }
    void HandleIdle() { }
    void HandleChase() { }
    void HandlePatrol() { }
    void HandleAttack() { }
}

public enum EnemyState
{
    Idle,
    Chase,
    Patrol,
    Attack,
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Animator animator;
    [Header("Enemy Basic parameters")]
    public float patrolSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;
    public Vector3 faceDirection;

    public Transform attacker;
    public float hurtForce;

    private float x;

    [Header("Attack Check")]
    public Vector2 centerOffset;
    public Vector2 sightCheckBoxSize;
    public float sightCheckBoxDistance;

    public LayerMask attackLayer;

    [Header("Timer For Partol")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait = false;

    [Header("Timer For Chase")]

    public float lostTargetTime;
    public float lostTargetTimeCounter;

    [Header("Enemy State")]
    public bool isHurt;
    public bool isDead;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Start()
    {
        x = transform.localScale.x;
        if (transform.localScale.x > 0)
        {
            x = math.abs(transform.localScale.x);
        }
        else
        {
            x = -math.abs(transform.localScale.x);
        }
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }

        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * Time.deltaTime * faceDirection.x, rb.velocity.y);
    }


    /// <summary>
    /// Update the time counter for the wait time
    /// </summary>
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDirection.x, 1, 1);
            }
        }

        if (!FoundPlayer())
        {
            lostTargetTimeCounter -= Time.deltaTime;
            lostTargetTimeCounter = math.max(0, lostTargetTimeCounter);
        }
        else
        {
            lostTargetTimeCounter = lostTargetTime;
        }
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, sightCheckBoxSize, 0, faceDirection, sightCheckBoxDistance, attackLayer);
    }

    /// <summary>
    /// Check if the target is in sight
    /// </summary>
    /// <returns>if player in sight returns true else false.</returns>
#if false
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
#endif
    public void SwitchState(EnemyState state)
    {
        var newState = state switch
        {
            EnemyState.patrol => patrolState,
            EnemyState.chase => chaseState,
            _ => null,
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    #region events
    public void OnTakenDamage(Transform attackTrans)
    {
        attacker = attackTrans;

        //Turn to face attacker
        if (attackTrans.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
        }
        if (attackTrans.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }

        //give a force when taking damage
        isHurt = true;
        animator.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(Hurt(dir));
    }

    /// <summary>
    /// The coroutine function is used to reset the invincibility time after receiving damage.
    /// </summary>
    /// <param name="dir">force direction</param>
    /// <returns></returns>
    private IEnumerator Hurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    /// <summary>
    /// when the enemy is dead, change the layer to 2 and set the dead bool to true.
    /// </summary>
    public void OnDead()
    {
        gameObject.layer = 2;
        animator.SetBool("dead", true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset, 0.2f);
        Gizmos.DrawWireCube(transform.position + (Vector3)centerOffset + new Vector3(-transform.localScale.x * sightCheckBoxDistance, 0, 0), sightCheckBoxSize);
    }
}
/// <summary>
/// The enum is used to switch the enemy state.
/// </summary>
public enum EnemyState
{
    patrol,
    chase,
    skill,
}
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Animator animator;
    [Header("Enemy Basic parameters")]
    public float patrolSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;

    public FaceDirection initialFaceDirection;
    [HideInInspector] public Vector3 initialLocalScale;
    public FaceDirection currentFaceDirection;
    [HideInInspector] public Vector2 moveDirection = Vector2.zero;
    [HideInInspector] public Transform attacker;
    public float hurtForce;

    [Header("Hitbox Check")]
    public Vector2 centerOffset;
    public Vector2 checkBoxSize;
    public float checkBoxDistance;
    public LayerMask targetLayer;
    [Header("Target Check")]
    public bool targetInSight = false;
    public bool targetInAttackRange = false;
    public bool targetChaseable = false;
    [Header("Target Around Distance and Check")]
    public float targetAroundDistance = 4f;
    public bool targetAround = false;

    [Header("Timer For Check Lost Target")]
    public GameObject target;
    public float lostTargetTime;
    public float lostTargetTimeCounter;
    private bool lostTarget = false;

    [Header("Enemy State")]
    public bool isHurt;
    public bool isDead;
    public EnemyState enemyState;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState attackState;
    protected BaseState skillState;
    protected BaseState idleState;
    protected BaseState dodgeState;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Start()
    {
        currentFaceDirection = initialFaceDirection;
        initialLocalScale = transform.localScale;
    }

    protected virtual void OnEnable()
    {
        currentState = idleState;
        currentState.OnEnter(this);
    }
    protected virtual void Update()
    {
        currentFaceDirection = transform.localScale.x * initialLocalScale.x > 0 ? initialFaceDirection : initialFaceDirection == FaceDirection.left ? FaceDirection.right : FaceDirection.left;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        UpdateTargetInformation();
        currentState.LogicUpdate();
        TimeCounter();
    }

    protected virtual void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    protected virtual void OnDisable()
    {
        currentState.OnExit();
    }

    protected virtual void Move()
    {
        moveDirection = new Vector2(currentFaceDirection == FaceDirection.right ? 1 : -1, moveDirection.y);
        rb.velocity = new Vector2(currentSpeed * Time.deltaTime * moveDirection.x, rb.velocity.y);
    }

    /// <summary>
    /// This function is called in Update() to get target information.
    /// </summary>
    protected virtual void UpdateTargetInformation()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        IsTargetInSight();
        IsLostTarget();
        IsTargetInAttackRange();
        IsTargetChaseable();
        IsTargetAround();
    }

    /// <summary>
    /// Update the time counters
    /// </summary>
    protected virtual void TimeCounter()
    {
        return;
    }

    /// <summary>
    /// If target is not in the sight for a certain time, 
    /// set lostTarget to true and set the timer to 0 .
    /// otherwise, set the timer to the lostTargetTime. And set lostTarget to false. 
    /// </summary>
    protected virtual void IsLostTarget()
    {
        if (!targetInSight)
        {
            lostTargetTimeCounter -= Time.deltaTime;
            lostTargetTimeCounter = math.max(0, lostTargetTimeCounter);
        }
        else
        {
            lostTargetTimeCounter = lostTargetTime;
        }
        lostTarget = lostTargetTimeCounter == 0;
    }

    /// <summary>
    /// the hitbox to sumulate the attack range or sight
    /// </summary>
    /// <returns>the bool for result </returns>
    protected bool IsHitBox()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkBoxSize, 0, moveDirection, checkBoxDistance, targetLayer);
    }
    /// <summary>
    /// Check if the target is in attack range also update the targetInAttackRange to true or false.
    /// </summary>
    /// <returns>targetInAttackRange</returns>
    public virtual bool IsTargetInAttackRange()
    {
        return targetInAttackRange = false;
    }
#if true
    /// <summary>
    /// Check if the target is in sight also update the targetInSight to true or false.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsTargetInSight()
    {
        return targetInSight = false;
    }
    public virtual bool IsTargetChaseable()
    {
        return targetChaseable = targetInSight && Mathf.Abs(transform.position.y - target.transform.position.y) <= 0.1f;
    }
    public virtual bool IsTargetAround()
    {
        return targetAround = Vector3.Distance(target.transform.position, transform.position) < targetAroundDistance;

    }
#endif
    /// <summary>
    /// Switch the state of the enemy.
    /// </summary>
    /// <param name="state">the state to switch</param>
    public void SwitchState(EnemyState state)
    {
        enemyState = state;
        var newState = state switch
        {
            EnemyState.idle => idleState,
            EnemyState.patrol => patrolState,
            EnemyState.chase => chaseState,
            EnemyState.skill => skillState,
            EnemyState.attack => attackState,
            EnemyState.dodge => dodgeState,
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
        if ((attackTrans.position.x > transform.position.x && currentFaceDirection == FaceDirection.left) || (attackTrans.position.x < transform.position.x && currentFaceDirection == FaceDirection.right))
        {
            //attacker is on the back
            //face to the attacker
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
        SwitchState(EnemyState.chase);
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
    /// <summary>
    /// This function is used to draw the sight box in the editor.
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset, 0.2f);
        Gizmos.DrawWireCube(transform.position + (Vector3)centerOffset + new Vector3((currentFaceDirection == FaceDirection.right ? 1 : -1) * checkBoxDistance, 0, 0), checkBoxSize);
    }
}
/// <summary>
/// The enum is used to switch the enemy state.
/// </summary>
public enum EnemyState
{
    idle,
    patrol,
    chase,
    skill,
    attack,
    dodge
}
public enum FaceDirection { left, right };
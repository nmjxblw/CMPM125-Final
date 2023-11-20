using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [HideInInspector] public Animator animator;
    [Header("Enemy Movement parameters")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;

    public Vector3 faceDirection;

    protected Transform attacker;

    [Header("Timer")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x, 0, 0);



        TimeCounter();
        currentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        if (!isHurt & !isDead)
        { Move(); }

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
    }

    public void OnTakenDamage(Transform attackTrans)
    {
        attacker = attackTrans;



        //Turn to face attacker
        if (attackTrans.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (attackTrans.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
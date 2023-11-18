using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    PhysicsCheck physicsCheck;
    [HideInInspector] public Animator animator;
    [Header("Enemy Movement parameters")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;

    public Vector3 faceDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x, 0, 0);
        if(physicsCheck.touchLeftWall || physicsCheck.touchRightWall)
        {
            transform.localScale = new Vector3(faceDirection.x, 1, 1);
        }
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * Time.deltaTime * faceDirection.x, rb.velocity.y);
    }
}
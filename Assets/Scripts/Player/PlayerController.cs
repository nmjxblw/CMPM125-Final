using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private PlayerAnimation playerAnimation;
    public Vector2 inputDirection;
    private Rigidbody2D rb;

    private PhysicsCheck physicsCheck;

    [Header("Player Movement parameters")]
    public float speed;
    public float jumpForce;
    public float hurtForce;

    [Header("Player State")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl.Gameplay.Jump.started += Jump;

        inputControl.Gameplay.Attack.started += PlayerAttack;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void JumpAttack(InputAction.CallbackContext obj)
    {
        if (!physicsCheck.isGrounded)
        {
            Debug.Log("AirAttack");
        }
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(other.name);
    // }

    private void Move()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime * inputDirection.x, rb.velocity.y);

        //flip player sprite
        Vector3 currentScale = transform.localScale;
        currentScale.x = inputDirection.x < 0 ? -math.abs(currentScale.x) : (inputDirection.x > 0 ? math.abs(currentScale.x) : currentScale.x);
        transform.localScale = new Vector3(currentScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;

    }

#region UnityEvent
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
        
    }
#endregion
}

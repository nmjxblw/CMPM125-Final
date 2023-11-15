using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;

    private PhysicsCheck physicsCheck;

    [Header("Player Movement parameters")]
    public float speed;
    public float jumpForce;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>(); 
        inputControl.Gameplay.Jump.started += Jump;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if(physicsCheck.isGrounded){
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime * inputDirection.x, rb.velocity.y);

        //flip player sprite
        Vector3 currentScale = transform.localScale;
        currentScale.x = inputDirection.x < 0 ? -math.abs(currentScale.x) : (inputDirection.x > 0 ? math.abs(currentScale.x) : currentScale.x);
        transform.localScale = new Vector3(currentScale.x, transform.localScale.y, transform.localScale.z);
    }


}

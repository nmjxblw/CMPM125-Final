using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoarController : PlayerController
{
    public float runSpeed;

    protected override void Awake()
    {
        base.Awake();
        inputControl.Gameplay.Attack.canceled += StopRun;
    }
    protected override void PlayerAttack(InputAction.CallbackContext context)
    {
        base.PlayerAttack(context);
        speed += runSpeed;
        
    }

    private void StopRun(InputAction.CallbackContext context)
    {
        speed -= runSpeed;
        isAttack = false;
    }

    protected override void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
    }
    

}

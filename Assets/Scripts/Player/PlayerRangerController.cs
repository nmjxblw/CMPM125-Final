using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRangerController : PlayerController
{
    protected override void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
    }

    protected override void PlayerAttack(InputAction.CallbackContext context)
    {
        if(rb.velocity.magnitude > 0.1f)
        {
            return;
        }
        base.PlayerAttack(context);

    }
}

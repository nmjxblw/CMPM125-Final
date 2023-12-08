using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierDodgeState : BaseState
{
    public Vector2 dodgeDirection;
    public bool dodgeAgain = true;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        ((Soldier)currentEnemy).isDodge = true;
        ((Soldier)currentEnemy).isJump = true;
        dodgeAgain = true;
        dodgeDirection = new Vector2((currentEnemy.currentFaceDirection == FaceDirection.right ? -1 : 1), 0);
        if (currentEnemy.physicsCheck.touchLeftWall || currentEnemy.physicsCheck.touchRightWall)
        {
            dodgeDirection = Vector2.zero;
        }
        currentEnemy.rb.AddForce(dodgeDirection * ((Soldier)currentEnemy).dodgeForce + new Vector2(0, 1) * ((Soldier)currentEnemy).jumpForce, ForceMode2D.Impulse);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.targetAtBack)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        if ((currentEnemy.physicsCheck.touchLeftWall || currentEnemy.physicsCheck.touchRightWall) && dodgeAgain)
        {
            dodgeAgain = false;
            currentEnemy.rb.AddForce(-dodgeDirection * ((Soldier)currentEnemy).dodgeForce, ForceMode2D.Impulse);
        }
        if (currentEnemy.physicsCheck.isGrounded && !dodgeAgain)
        {
            dodgeAgain = true;
        }
        if (currentEnemy.rb.velocity.magnitude <= 0.1f)
        {
            currentEnemy.SwitchState(EnemyState.chase);
        }
    }
    public override void PhysicsUpdate()
    {
        Vector2 frictionForce = new Vector2(-currentEnemy.rb.velocity.x * ((Soldier)currentEnemy).friction, 0);
        currentEnemy.rb.AddForce(frictionForce + new Vector2(0, 1f), ForceMode2D.Force);
    }
    public override void OnExit()
    {
        ((Soldier)currentEnemy).isDodge = false;
        ((Soldier)currentEnemy).dodgeTimer = ((Soldier)currentEnemy).dodgeCooldown;
    }
}
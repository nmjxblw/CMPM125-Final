using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerDodgeState : BaseState
{
    public int dir;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        ((Ranger)currentEnemy).isDodge = true;
        currentEnemy.currentSpeed = ((Ranger)currentEnemy).dodgeSpeed;
        currentEnemy.animator.SetBool("run", true);
        currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
    }

    public override void LogicUpdate()
    {
        if (HitWallCheck() || !currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.currentFaceDirection == FaceDirection.left) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.currentFaceDirection == FaceDirection.right) || currentEnemy.lostTargetTimeCounter <= 0)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        if (Vector3.Distance(currentEnemy.target.transform.position, currentEnemy.transform.position) >= currentEnemy.targetAroundDistance + 2f)
        {
            currentEnemy.SwitchState(EnemyState.idle);
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        if (currentEnemy.targetAtBack)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        ((Ranger)currentEnemy).isDodge = false;
        currentEnemy.animator.SetBool("run", false);
    }

    public bool HitWallCheck()
    {
        return Physics2D.OverlapCircle((Vector2)currentEnemy.target.transform.position +
            new Vector2(currentEnemy.targetAroundDistance * (currentEnemy.currentFaceDirection == FaceDirection.right ? 1 : -1),
                 currentEnemy.centerOffset.y), 0.2f, currentEnemy.physicsCheck.groundLayer);
    }
}
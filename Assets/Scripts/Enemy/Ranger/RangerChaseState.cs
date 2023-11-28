using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.targetInSight && currentEnemy.targetInAttackRange)
        {
            currentEnemy.SwitchState(EnemyState.attack);
            return;
        }
        if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.currentFaceDirection == FaceDirection.left) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.currentFaceDirection == FaceDirection.right) || currentEnemy.lostTargetTimeCounter <= 0)
        {
            currentEnemy.SwitchState(EnemyState.patrol);
            return;
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("run", false);
    }
}

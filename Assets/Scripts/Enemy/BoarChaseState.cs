using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if(currentEnemy.lostTargetTimeCounter <= 0 ){
            currentEnemy.SwitchState(EnemyState.patrol);
            return;
        }
        if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDirection.x < 0) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDirection.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDirection.x, 1, 1);
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("run", false);
    }
}

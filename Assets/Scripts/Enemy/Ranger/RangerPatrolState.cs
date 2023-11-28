using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.patrolSpeed;
    }

    public override void LogicUpdate()
    {
        // if the target is in sight but out of attack range, then switch to chase state
        if (currentEnemy.targetInSight && !currentEnemy.targetInAttackRange && currentEnemy.transform.position.y >= currentEnemy.target.transform.position.y)
        {
            currentEnemy.SwitchState(EnemyState.chase);
            return;
        }
        if (currentEnemy.targetInSight && currentEnemy.targetInAttackRange)
        {
            currentEnemy.SwitchState(EnemyState.attack);
            return;
        }
        if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.currentFaceDirection == FaceDirection.left) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.currentFaceDirection == FaceDirection.right))
        {
            ((Ranger)currentEnemy).wait = true;
            currentEnemy.animator.SetBool("run", false);
        }
        else
        {
            currentEnemy.animator.SetBool("run", true);
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("run", false);
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangerPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.patrolSpeed;
        currentEnemy.animator.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.targetInAttackRange && currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.attack);
            return;
        }
        else if (currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.chase);
            return;
        }
        if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.currentFaceDirection == FaceDirection.left) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.currentFaceDirection == FaceDirection.right))
        {
            currentEnemy.SwitchState(EnemyState.idle);
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {

        currentEnemy.animator.SetBool("run", false);
    }
}
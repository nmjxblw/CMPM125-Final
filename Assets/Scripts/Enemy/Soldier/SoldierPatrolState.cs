using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.patrolSpeed;
    }

    public override void LogicUpdate()
    {
        if (((Soldier)currentEnemy).triggerDodge && ((Soldier)currentEnemy).dodgeTimer == 0)
        {
            currentEnemy.SwitchState(EnemyState.dodge);
            return;
        }
        if (currentEnemy.targetChaseable && currentEnemy.targetInAttackRange)
        {
            currentEnemy.SwitchState(EnemyState.attack);
            return;
        }
        else if (currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.chase);
            return;
        }
        else if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.currentFaceDirection == FaceDirection.left) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.currentFaceDirection == FaceDirection.right))
        {
            currentEnemy.SwitchState(EnemyState.idle);
        }
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
    }

    public override void LogicUpdate()
    {
        if (Vector3.Distance(currentEnemy.transform.position, currentEnemy.target.transform.position) >= 4f && currentEnemy.targetAtBack)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        if (((Soldier)currentEnemy).triggerDodge && ((Soldier)currentEnemy).dodgeTimer == 0)
        {
            currentEnemy.SwitchState(EnemyState.dodge);
            return;
        }
        if (currentEnemy.targetInAttackRange && currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.attack);
            return;
        }
        if (!currentEnemy.physicsCheck.isGrounded || currentEnemy.physicsCheck.touchLeftWall ||
         currentEnemy.physicsCheck.touchRightWall || currentEnemy.lostTargetTimeCounter <= 0)
        {
            currentEnemy.SwitchState(EnemyState.patrol);
            return;
        }
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
    }
}
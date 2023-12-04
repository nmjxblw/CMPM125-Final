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
        if (currentEnemy.targetInAttackRange && currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.attack);
            return;
        }
        if (!currentEnemy.targetChaseable || currentEnemy.lostTargetTimeCounter <= 0)
        {
            currentEnemy.SwitchState(EnemyState.idle);
            return;
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("run", false);
    }
}

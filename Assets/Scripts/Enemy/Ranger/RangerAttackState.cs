using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttackState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.animator.SetBool("attack", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy Ranger Shoot") &&
            currentEnemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (currentEnemy.targetAround && currentEnemy.targetChaseable)
            {
                currentEnemy.SwitchState(EnemyState.dodge);
                return;
            }
            currentEnemy.SwitchState(EnemyState.idle);
            return;
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("attack", false);
    }
}
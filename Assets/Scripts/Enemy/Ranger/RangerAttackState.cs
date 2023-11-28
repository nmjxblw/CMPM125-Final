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
        // if the target is in sight but out of attack range, then switch to chase state
        // else if the attack animation is done, switch to chase state
        if (currentEnemy.targetInSight && !currentEnemy.targetInAttackRange)
        {
            // ||
            // (currentEnemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Ranger_shoot") &&
            // currentEnemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            currentEnemy.SwitchState(EnemyState.chase);
            return;
        }
        //if target is out of range and out of attack range, switch to patrol state
        if ((!currentEnemy.targetInSight && !currentEnemy.targetInAttackRange) || currentEnemy.lostTargetTimeCounter <= 0)
        {
            currentEnemy.SwitchState(EnemyState.patrol);
            return;
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("attack", false);
    }
}
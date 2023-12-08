using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierIdleState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        ((Soldier)currentEnemy).wait = true;
        ((Soldier)currentEnemy).waitTimeCounter = ((Soldier)currentEnemy).waitTime;
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
        }
        else if (currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.chase);
        }
        if (((Soldier)currentEnemy).wait)
        {
            ((Soldier)currentEnemy).waitTimeCounter -= Time.deltaTime;
            if (((Soldier)currentEnemy).waitTimeCounter <= 0 || currentEnemy.isHurt)
            {
                currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
            }
            else { return; }
        }
        currentEnemy.SwitchState(EnemyState.patrol);
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
        ((Soldier)currentEnemy).wait = false;
        ((Soldier)currentEnemy).waitTimeCounter = ((Soldier)currentEnemy).waitTime;
    }
}
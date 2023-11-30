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
        currentEnemy.animator.SetBool("run", false);
        currentEnemy.animator.SetBool("attack", false);
    }

    public override void LogicUpdate()
    {
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
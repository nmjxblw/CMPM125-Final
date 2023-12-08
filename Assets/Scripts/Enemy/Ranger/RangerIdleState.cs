using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangerIdleState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        ((Ranger)currentEnemy).wait = true;
        ((Ranger)currentEnemy).waitTimeCounter = ((Ranger)currentEnemy).waitTime;
        currentEnemy.currentSpeed = 0f;
        currentEnemy.animator.SetBool("run", false);
        currentEnemy.animator.SetBool("attack", false);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.targetAround && currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.dodge);
            return;
        }
        else if(currentEnemy.targetInAttackRange && currentEnemy.targetChaseable){
            currentEnemy.SwitchState(EnemyState.attack);
        }
        else if (currentEnemy.targetChaseable)
        {
            currentEnemy.SwitchState(EnemyState.chase);
            return;
        }
        else if (((Ranger)currentEnemy).wait)
        {
            ((Ranger)currentEnemy).waitTimeCounter -= Time.deltaTime;
            if (((Ranger)currentEnemy).waitTimeCounter <= 0 || currentEnemy.isHurt)
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
        ((Ranger)currentEnemy).wait = false;
        ((Ranger)currentEnemy).waitTimeCounter = ((Ranger)currentEnemy).waitTime;
    }
}

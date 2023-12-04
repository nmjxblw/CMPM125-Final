using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.patrolSpeed;
        currentEnemy.animator.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if(currentEnemy.targetChaseable){
            currentEnemy.SwitchState(EnemyState.chase);
        }
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("run", false);
    }
}
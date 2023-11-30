using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("run", false);
    }
}
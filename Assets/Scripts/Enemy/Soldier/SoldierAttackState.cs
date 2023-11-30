using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierAttackState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        currentEnemy.animator.SetBool("isAttack", true);
    }

    public override void LogicUpdate()
    {

    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isAttack", false);
    }
}
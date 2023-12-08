using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy as Boar;
        currentEnemy.currentSpeed = currentEnemy.patrolSpeed;
        currentEnemy.animator.SetBool("walk", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.IsTargetInSight())
        {
            currentEnemy.SwitchState(EnemyState.chase);
            return;
        }
        if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.IsFaceAgainstWall()))
        {
            currentEnemy.SwitchState(EnemyState.idle);
        }
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("walk", false);
    }
}
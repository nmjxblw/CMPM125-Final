using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }
    public override void OnExit() { }
    public override void LogicUpdate()
    {
        if ((currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDirection.x < 0) ||
         (currentEnemy.physicsCheck.touchRightWall && CurrentEnemy.faceDirection.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.animator.SetBool("walk", false);
        }
    }
    public override void PhysicsUpdate() { }
}
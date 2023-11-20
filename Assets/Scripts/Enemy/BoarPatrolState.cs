using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }
    public override void OnExit() 
    {
        currentEnemy.animator.SetBool("walk", false);
    }
    public override void LogicUpdate()
    {
        if (!currentEnemy.physicsCheck.isGrounded || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDirection.x < 0) ||
         (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDirection.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.animator.SetBool("walk", false);
        }
        else{
            currentEnemy.animator.SetBool("walk", true);
        }
    }
    public override void PhysicsUpdate() { }
}
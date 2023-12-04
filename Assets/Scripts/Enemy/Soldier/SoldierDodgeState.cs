using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierDodgeState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        ((Soldier)currentEnemy).isDodge = true;
    }

    public override void LogicUpdate()
    {
        // ((Soldier)currentEnemy).isJump = !currentEnemy.physicsCheck.isGrounded;
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
       ((Soldier)currentEnemy).isDodge = false;
    }
}
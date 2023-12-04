using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoarIdleState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        ((Boar)currentEnemy).wait = true;
        currentEnemy.animator.SetBool("walk", false);
        currentEnemy.animator.SetBool("run", false);
    }

    public override void LogicUpdate()
    {
        if (((Boar)currentEnemy).wait)
        {
            ((Boar)currentEnemy).waitTimeCounter -= Time.deltaTime;
            if (((Boar)currentEnemy).waitTimeCounter <= 0 || currentEnemy.isHurt)
            {
                ((Boar)currentEnemy).wait = false;
                ((Boar)currentEnemy).waitTimeCounter = ((Boar)currentEnemy).waitTime;
                currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
            }
            else { return; }
        }
        currentEnemy.SwitchState(EnemyState.patrol);
    }
    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
    }
}

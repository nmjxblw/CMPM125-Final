using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierAttackState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        ((Soldier)currentEnemy).isAttack = true;
        currentEnemy.animator.SetTrigger("attack");
        ((Soldier)currentEnemy).attackTimer = 0f;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.targetAtBack)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        if (((Soldier)currentEnemy).triggerDodge && ((Soldier)currentEnemy).dodgeTimer == 0)
        {
            currentEnemy.SwitchState(EnemyState.dodge);
            return;
        }
        if (((Soldier)currentEnemy).attackTimer > 0)
        {
            return;
        }
        else
        {
            if (currentEnemy.targetInAttackRange && currentEnemy.targetChaseable)
            {
                currentEnemy.animator.SetTrigger("attack");
                if (currentEnemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy Soldier Attack2"))
                {
                    ((Soldier)currentEnemy).attackTimer = ((Soldier)currentEnemy).attackCooldown;
                }
                return;
            }
        }
        currentEnemy.SwitchState(EnemyState.chase);
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
    }


}
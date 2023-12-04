using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierAttackState : BaseState
{
    public bool targetAtBack;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0f;
        currentEnemy.animator.SetBool("isAttack", true);
        currentEnemy.animator.SetTrigger("attack");
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .8f && currentEnemy.targetInAttackRange && currentEnemy.targetChaseable)
        {
            currentEnemy.animator.SetTrigger("attack");
            return;
        }
        if (currentEnemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (currentEnemy.targetChaseable)
            { currentEnemy.SwitchState(EnemyState.chase); }
            else
            {
                if (IsTargetAtBack())
                {
                    currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
                }
                currentEnemy.SwitchState(EnemyState.idle);
            }
        }
    }
    public override void PhysicsUpdate() { }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isAttack", false);
    }

    public bool IsTargetAtBack()
    {
        return targetAtBack = (currentEnemy.transform.position.x > currentEnemy.target.transform.position.x && currentEnemy.transform.localScale.x > 0) ||
            (currentEnemy.transform.position.x < currentEnemy.target.transform.position.x && currentEnemy.transform.localScale.x < 0);
    }
}
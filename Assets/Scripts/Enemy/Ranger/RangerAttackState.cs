// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RangerAttackState : BaseState
// {
//     public override void OnEnter(Enemy enemy)
//     {
//         currentEnemy = enemy;
//         currentEnemy.currentSpeed = 0;
//         currentEnemy.animator.SetBool("shoot", true);
//     }

//     public override void LogicUpdate()
//     {
//         // if the target is in sight but out of attack range, then switch to chase state
//         if (currentEnemy.IsTargetInSight(currentEnemy.rangerSightOffset) && !currentEnemy.IsTargetInAttackRange())
//         {
//             currentEnemy.SwitchState(EnemyState.chase);
//             return;
//         }
//         //if target is out of range and out of attack range, switch to patrol state
//         if ((!currentEnemy.IsTargetInSight(currentEnemy.rangerSightOffset) && !currentEnemy.IsTargetInAttackRange()) || currentEnemy.lostTargetTimeCounter <= 0)
//         {
//             currentEnemy.SwitchState(EnemyState.patrol);
//             return;
//         }
//     }
//     public override void PhysicsUpdate() { }

//     public override void OnExit()
//     {
//         currentEnemy.animator.SetBool("shoot", false);
//     }
// }
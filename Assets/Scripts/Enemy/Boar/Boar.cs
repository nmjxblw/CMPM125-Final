using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boar : Enemy
{
    [Header("Timer For Partol")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait = false;
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
        idleState = new BoarIdleState();
        waitTimeCounter = waitTime;
    }

    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }
        base.FixedUpdate();
    }

    public override bool IsTargetInSight(){
        return targetInSight = IsHitBox();
    }

    public override bool IsTargetInAttackRange(){
        return  targetInAttackRange = IsHitBox();
    }
    
}

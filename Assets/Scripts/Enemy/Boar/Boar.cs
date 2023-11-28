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
    }

    protected override void TimeCounter()
    {
        UpdateWaitTimer();
        base.TimeCounter();
    }

    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }
        base.FixedUpdate();
    }

    protected override void UpdateWaitTimer()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            }
        }
    }

    public override bool IsTargetInSight(){
        return targetInSight = IsHitBox();
    }

    public override bool IsTargetInAttackRange(){
        return  targetInAttackRange = IsHitBox();
    }
    
}

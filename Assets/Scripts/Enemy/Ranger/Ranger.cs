using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Enemy
{
    [Header("Ranger Setting")]
    public Vector2 rangerSightOffset;
    [Header("Ranger View Setting")]
    public float rangerViewAngle = 90f;
    public float rangerViewRadius = 20f;
    [Header("Timer For Partol")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait = false;
    public bool shoot = false;
    protected override void Awake()
    {
        base.Awake();
        patrolState = new RangerPatrolState();
        chaseState = new RangerChaseState();
        attackState = new RangerAttackState();
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
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void ShootArrow()
    {
        //Instantite the arrow prefab
        Debug.Log("Shoot Arrow");
    }

    public override bool IsTargetInSight()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x + rangerSightOffset.x, transform.position.y + rangerSightOffset.y);

        for (int i = 0; i < 180; i++)
        {
            float angle = transform.rotation.eulerAngles.z - rangerViewAngle / 2 + i * (rangerViewAngle / 180f);
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * (transform.localScale.x > 0 ? 1 : -1), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rangerViewRadius, attackLayer);
            Debug.DrawRay(rayOrigin, rayDirection * rangerViewRadius, Color.red);
            if (hit.collider != null && hit.collider.gameObject == target)
            {

                return targetInSight = true;
            }
        }
        return targetInSight = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position + (Vector3)rangerSightOffset, 0.1f);
    }

    public override bool IsTargetInAttackRange()
    {
        return targetInAttackRange = IsHitBox();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    [Header("Soldier Setting")]
    public Vector2 soldierSightOffset = new Vector2(0f, 1.7f);
    [Header("Soldier View Setting")]
    public float soldierViewAngle = 90f;
    public float soldierViewRadius = 20f;
    [Header("Timer For Soldier Partol")]
    public float waitTime = 2f;
    public float waitTimeCounter = 2f;
    [Header("Soldier Properties Checker")]
    public bool wait = false;
    public bool shoot = false;
    public bool isAttack = false;
    [Header("Soldier Dodge Setting")]
    public float jumpForce = 5f;
    public bool isJump = false;
    public float dodgeForce = 5f;
    public bool isDodge = false;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new SoldierPatrolState();
        chaseState = new SoldierChaseState();
        attackState = new SoldierAttackState();
        idleState = new SoldierIdleState();
    }

    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait && !isDodge && !isAttack)
        {
            Move();
        }
        base.FixedUpdate();
    }

    public override bool IsTargetInSight()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x + soldierSightOffset.x, transform.position.y + soldierSightOffset.y);

        for (int i = 0; i < 180; i++)
        {
            float angle = transform.rotation.eulerAngles.z - soldierViewAngle / 2 + i * (soldierViewAngle / 180f);
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * (transform.localScale.x > 0 ? 1 : -1), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, soldierViewRadius, targetLayer);
            Debug.DrawRay(rayOrigin, rayDirection * soldierViewRadius, Color.red);
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
        Gizmos.DrawWireSphere(transform.position + (Vector3)soldierSightOffset, 0.1f);
    }

    public override bool IsTargetInAttackRange()
    {
        return targetInAttackRange = IsHitBox();
    }
}

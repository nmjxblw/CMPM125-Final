using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
public class Ranger : Enemy
{
    [Header("Ranger Setting")]
    public Vector2 rangerSightOffset;
    [Header("Ranger View Setting")]
    public float rangerViewAngle = 90f;
    public float rangerViewRadius = 20f;
    [Header("Timer For Ranger Partol")]
    public float waitTime;
    public float waitTimeCounter;
    [Header("Ranger Properties Checker")]
    public bool wait = false;
    public bool shoot = false;
    public bool isAttack = false;
    [Header("Ranger Dodge Setting")]
    [HideInInspector] public float dodgeSpeed;
    public bool isDodge = false;
    protected override void Awake()
    {
        base.Awake();
        patrolState = new RangerPatrolState();
        chaseState = new RangerChaseState();
        attackState = new RangerAttackState();
        idleState = new RangerIdleState();
        dodgeState = new RangerDodgeState();
        waitTimeCounter = waitTime;
        dodgeSpeed = chaseSpeed * 2f;
    }
    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead)
        {
            Move();
        }
        base.FixedUpdate();
    }

    public void ShootArrow()
    {
        //Instantite the arrow prefab
        isAttack = true;
        GameObject arrow = EnemyArrowPool.SharedInstance.GetPooledObject();
        arrow.GetComponent<EnemyArrow>().rangerDirection = transform.localScale.x;
        if (arrow != null)
        {
            arrow.transform.position = transform.position + (Vector3)centerOffset + new Vector3(currentFaceDirection == FaceDirection.right ? 2f : -2f, 0, 0);
            arrow.transform.rotation = transform.rotation;

            arrow.SetActive(true);
        }
    }

    public override bool IsTargetInSight()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x + rangerSightOffset.x, transform.position.y + rangerSightOffset.y);

        for (int i = 0; i < 180; i++)
        {
            float angle = transform.rotation.eulerAngles.z - rangerViewAngle / 2 + i * (rangerViewAngle / 180f);
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * (currentFaceDirection == FaceDirection.right? 1 : -1), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rangerViewRadius, targetLayer);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    [Header("Enemy Soldier Debug Check")]
    public bool wait = false;
    public bool isAttack = false;
    public bool arrowInSight = false;
    public bool isJump = false;
    public bool isDodge = false;
    [Header("Soldier Setting")]
    public Vector2 soldierSightOffset = new Vector2(0f, 1.7f);
    [Header("Soldier View Setting")]
    public float soldierViewAngle = 90f;
    public float soldierViewRadius = 20f;
    [Header("Timer For Soldier Partol")]
    public float waitTime = 2f;
    public float waitTimeCounter = 2f;
    [Header("Soldier Properties Checker")]
    public float attackCooldown = 2f;
    public float attackTimer = 2f;

    [Header("Soldier Dodge Setting")]
    public LayerMask arrowLayer = new LayerMask();
    public float dodgeCooldown = 5f;
    public float dodgeTimer = 5f;
    public float jumpForce = 7f;
    public float dodgeForce = 5f;
    public float friction = 3f;
    public bool triggerDodge;


    protected override void Awake()
    {
        base.Awake();
        patrolState = new SoldierPatrolState();
        chaseState = new SoldierChaseState();
        attackState = new SoldierAttackState();
        idleState = new SoldierIdleState();
        dodgeState = new SoldierDodgeState();
        dodgeTimer = dodgeCooldown;
    }

    protected override void Update()
    {
        IsArrowInSight();
        bool checkAttack = TransformManager.Instance.currentPlayer?TransformManager.Instance.currentPlayer.GetComponent<PlayerController>().isAttack : false;
        triggerDodge = (targetAround && checkAttack) || arrowInSight;
        base.Update();
        animator.SetFloat("vx", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vy", rb.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGrounded);
        dodgeTimer = Mathf.Clamp((dodgeTimer - Time.deltaTime), 0, dodgeCooldown);
        attackTimer = Mathf.Clamp((attackTimer - Time.deltaTime), 0, attackCooldown);
    }

    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !isDodge)
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

    public bool IsArrowInSight()
    {
        return arrowInSight = Physics2D.BoxCast(transform.position + (Vector3)centerOffset, new Vector2(8f, 2f), 0, moveDirection, 4f, arrowLayer);
    }
}

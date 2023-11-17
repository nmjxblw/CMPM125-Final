using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    [Header("Ground Check")]
    public bool isGrounded;
    public bool touchLeftWall;
    public bool touchRightWall;

    [Header("Detection parameters")]
    public bool manual;
    public float groundCheckRadius;

    public LayerMask groundLayer;

    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    // Start is called before the first frame update

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if(!manual)
        {
            rightOffset = new Vector2((capsuleCollider.bounds.size.x + capsuleCollider.offset.x) / 2, capsuleCollider.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();

    }

    void Check()
    {
        //check ground
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundCheckRadius, groundLayer);

        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, groundCheckRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, groundCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, groundCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, groundCheckRadius);
    }
}

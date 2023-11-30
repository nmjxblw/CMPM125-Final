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
    public float checkRaduis;

    public LayerMask groundLayer;

    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    // Start is called before the first frame update

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2(capsuleCollider.offset.x + capsuleCollider.size.x / 2, capsuleCollider.bounds.size.y / 2);
            leftOffset = new Vector2(capsuleCollider.offset.x - capsuleCollider.size.x / 2, rightOffset.y);
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
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaduis, groundLayer);

        //check wall
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset.x, leftOffset.y), checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(rightOffset.x, rightOffset.y), checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(leftOffset.x, leftOffset.y), checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(rightOffset.x, rightOffset.y), checkRaduis);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public float raycastDistance = 10f;
    private float originDir;

    // Start is called before the first frame update
    void Start()
    {
        originDir = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rayDirection;
        RaycastHit2D hit;
        if (originDir > 0)
        {
            if (transform.localScale.x > 0)
            {
                rayDirection = Vector2.right;
                hit = Physics2D.Raycast(transform.position + new Vector3(1.3f, 0.2f, 0), rayDirection, raycastDistance);
                Debug.DrawLine(transform.position + new Vector3(1.3f, 0.2f, 0), transform.position + new Vector3(1.3f, 0.2f, 0) + (Vector3)rayDirection * raycastDistance, Color.green);
            }
            else
            {
                rayDirection = Vector2.left;
                hit = Physics2D.Raycast(transform.position + new Vector3(-1.3f, 0.2f, 0), rayDirection, raycastDistance);
                Debug.DrawLine(transform.position + new Vector3(-1.3f, 0.2f, 0), transform.position + new Vector3(-1.3f, 0.2f, 0) + (Vector3)rayDirection * raycastDistance, Color.green);
            }
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                rayDirection = Vector2.left;
                hit = Physics2D.Raycast(transform.position + new Vector3(-1.3f, 0.2f, 0), rayDirection, raycastDistance);
                Debug.DrawLine(transform.position + new Vector3(-1.3f, 0.2f, 0), transform.position + new Vector3(-1.3f, 0.2f, 0) + (Vector3)rayDirection * raycastDistance, Color.green);
            }
            else
            {
                rayDirection = Vector2.right;
                hit = Physics2D.Raycast(transform.position + new Vector3(1.3f, 0.2f, 0), rayDirection, raycastDistance);
                Debug.DrawLine(transform.position + new Vector3(1.3f, 0.2f, 0), transform.position + new Vector3(1.3f, 0.2f, 0) + (Vector3)rayDirection * raycastDistance, Color.green);
            }
        }

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Soldier"))
            {

                TransformManager.Instance.isLookingSoldier = true;
            }

            if (hit.collider.CompareTag("Boar"))
            {

                TransformManager.Instance.isLookingBoar = true;
            }

            if (hit.collider.CompareTag("Ranger"))
            {

                TransformManager.Instance.isLookingRanger = true;
            }
        }
    }
}

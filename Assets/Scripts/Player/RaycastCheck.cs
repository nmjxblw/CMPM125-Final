using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public float raycastDistance = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rayDirection = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, raycastDistance);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Soldier"))
            {
                TransformManager.Instance.SetHitTag("Soldier");
            }
        }
        else
        {
            TransformManager.Instance.SetHitTag();
        }
        
        
    }
}

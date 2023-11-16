using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    public GameObject target;
    public LayerMask playerLayer = new LayerMask();
    private Transform targetTrasform;
    private Vector3 Destination;

    private void Awake()
    {
        target = target ? target : GameObject.FindGameObjectWithTag("Player");
        targetTrasform = target.transform;
        Destination = transform.position;
    }


    private bool isTargetInSight()
    {

        Vector2 origin = new Vector2(transform.position.x, transform.position.y + 0.95f);
        float viewAngle = 60f;
        float viewRadius = 10f;

        for (int i = 0; i < 180; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + i * (viewAngle / 180f);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewRadius, playerLayer);
            Debug.DrawRay(origin, direction * viewRadius, Color.red);
            if (hit.collider != null && hit.collider.transform == targetTrasform)
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        // change state only
        if (isTargetInSight())
        {
            Destination = targetTrasform.position;
        }
        else
        {
            Destination = transform.position;
        }
    }

    void MoveToDestination(){
        
    }
}

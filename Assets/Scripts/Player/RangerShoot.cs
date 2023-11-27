using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerShoot : MonoBehaviour
{
    private GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        arrow = ArrowPool.SharedInstance.GetPooledObject();
        if (arrow != null)
        {
            arrow.transform.position = transform.position;
            arrow.transform.rotation = transform.rotation;

            arrow.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
        
}

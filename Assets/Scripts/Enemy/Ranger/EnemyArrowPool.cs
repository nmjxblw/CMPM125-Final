using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Modified based on the object pool tutorial provided in sides
public class EnemyArrowPool : MonoBehaviour
{
    public static EnemyArrowPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject arrowToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(arrowToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            tmp.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}

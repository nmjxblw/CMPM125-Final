using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        TransformManager.Instance.ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

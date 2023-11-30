using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    public GameObject player;
    private CinemachineVirtualCamera vc;
    // Update is called once per frame

    void Start(){
        vc = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        vc.Follow = player.transform;
    }
}

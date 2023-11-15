using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TransformManager : MonoBehaviour
{
    public static TransformManager Instance { get; private set; }

    public PlayerInputControl inputControl;

    public GameObject currentPlayer;
    public GameObject soldierPrefab;

    public string hitTag;

    public CinemachineVirtualCamera cinemachineCamera;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // inputControl = new PlayerInputControl();

        // inputControl.Gameplay.Jump.started += TransformInput;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = currentPlayer.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TransformInput();
    }

    public void SetHitTag(string hitTag)
    {
        this.hitTag = hitTag;
    }

    public void SetHitTag()
    {
        this.hitTag = null;
    }

    public void TransformCharacter(GameObject transformPrefab, Vector3 position)
    {

        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }


        currentPlayer = Instantiate(soldierPrefab, position, Quaternion.identity);
        cinemachineCamera.Follow = currentPlayer.transform;

    }

    public void TransformInput()
    {

        

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Transform Input");
            if (hitTag != null)
            {
                if (hitTag == "Soldier")
                {
                    TransformCharacter(soldierPrefab, currentPlayer.transform.position);
                }
            }
        }

    }
}

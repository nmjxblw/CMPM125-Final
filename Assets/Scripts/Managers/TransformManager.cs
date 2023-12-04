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
    public GameObject slimePrefab;
    public GameObject BoarPrefab;
    public GameObject soldierPrefab;
    public GameObject RangerPrefab;

    public bool isLookingBoar;
    public bool isLookingSoldier;
    public bool isLookingRanger;

    public string hitTag;

    public CinemachineVirtualCamera cinemachineCamera;

    public float maxHealth = 100;
    public float currentHealth;

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
        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Transform.started += TransformInput;
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
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

    public void TransformInput(InputAction.CallbackContext obj)
    {
        if (hitTag != null)
        {
            if (hitTag == "Soldier")
            {
                TransformCharacter(soldierPrefab, currentPlayer.transform.position);

            }

            if (hitTag == "Boar")
            {
                TransformCharacter(soldierPrefab, currentPlayer.transform.position);
            }
        }

    }
}

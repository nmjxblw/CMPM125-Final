using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TransformManager : MonoBehaviour
{
    public static TransformManager Instance { get; private set; }

    public PlayerInputControl inputControl;

    public GameObject currentPlayer;
    public GameObject slimePrefab;
    public GameObject BoarPrefab;
    public GameObject soldierPrefab;
    public GameObject RangerPrefab;
    public float currentPlayerIndex = 0;
    private Transform currentTransform;

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
        currentHealth = maxHealth;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        if (inputControl != null) { 
            inputControl.Disable();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = currentPlayer.transform;
        }
        currentPlayer = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        deadLineCheck();
        currentPlayer = GameObject.FindWithTag("Player");
        UpdateCamera();
    }

    private void deadLineCheck() {
        if (currentPlayer != null && currentPlayer.transform.position.y < -15) {
            SceneLoader.Instance.RestartScene();
        }
    }

    public void transformer(float order)
    {
        
        if (currentPlayer == null) return;
        GameObject oldPlayer = currentPlayer;
        currentTransform = currentPlayer.transform;
        if (order == 0)
        {
            if (currentPlayerIndex == 0)
            {
                return;
            }
            else
            {
                AudioManager.Instance.PlayTransformIntoClip();
                currentPlayer = Instantiate(slimePrefab, currentPlayer.transform.position, currentPlayer.transform.rotation);
                //currentPlayer.GetComponent<PlayerCharacter>().currentHealth = currentHealth;
                Destroy(oldPlayer);

                currentPlayerIndex = order;
            }
        }
        else if (order == 1 && isLookingSoldier)
        {

            if (currentPlayerIndex == 1)
            {
                return;
            }
            else
            {
                AudioManager.Instance.PlayTransformIntoClip();
                currentPlayer = Instantiate(soldierPrefab, currentTransform.position, currentTransform.rotation);
                //currentPlayer.GetComponent<PlayerCharacter>().currentHealth = currentHealth;
                Destroy(oldPlayer);

                currentPlayerIndex = order;
                Debug.Log("Solider");
            }
        }
        else if (order == 2 && isLookingBoar)
        {
            if (currentPlayerIndex == 2)
            {
                
                return;
            }
            else
            {
                AudioManager.Instance.PlayTransformIntoClip();
                currentPlayer = Instantiate(BoarPrefab, currentPlayer.transform.position, currentPlayer.transform.rotation);
                //currentPlayer.GetComponent<PlayerCharacter>().currentHealth = currentHealth;
                Destroy(oldPlayer);

                currentPlayerIndex = order;
                
            }
        }
        else if (order == 3 && isLookingRanger)
        {

            if (currentPlayerIndex == 3)
            {
                return;
            }
            else
            {
                AudioManager.Instance.PlayTransformIntoClip();
                currentPlayer = Instantiate(RangerPrefab, currentPlayer.transform.position, currentPlayer.transform.rotation);
                //currentPlayer.GetComponent<PlayerCharacter>().currentHealth = currentHealth;
                Destroy(oldPlayer);
            
                currentPlayerIndex = order;
                
            }
        }

        UpdateCamera();
    }

    public void ResetGame()
    {
        currentPlayer = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
        currentPlayerIndex = 0;
        isLookingBoar = false;
        isLookingSoldier = false;
        isLookingRanger = false;

        UpdateCamera();
    }

    public void UpdateCamera()
    {
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = currentPlayer.transform;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Instance.currentPlayerIndex = 0;
    }
}

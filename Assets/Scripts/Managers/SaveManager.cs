using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData playerData = new PlayerData();
        playerData.level = SceneManager.GetActiveScene().buildIndex;
        playerData.health = TransformManager.Instance.currentHealth;
        playerData.isLookingBoar = TransformManager.Instance.isLookingBoar;
        playerData.isLookingSoldier = TransformManager.Instance.isLookingSoldier;
        playerData.isLookingRanger = TransformManager.Instance.isLookingRanger;
        playerData.currentPlayer = TransformManager.Instance.currentPlayer;
        SaveLoadManager.SaveData(playerData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData()
    {
        PlayerData data = SaveLoadManager.LoadData();
        SceneManager.LoadScene(data.level);
        TransformManager.Instance.currentHealth = data.health;
        TransformManager.Instance.isLookingBoar = data.isLookingBoar;
        TransformManager.Instance.isLookingSoldier = data.isLookingSoldier;
        TransformManager.Instance.isLookingRanger = data.isLookingRanger;
        TransformManager.Instance.currentPlayer = data.currentPlayer;
    }
}

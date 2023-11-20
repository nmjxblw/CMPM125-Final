using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance = null;
    public static SceneLoader Instance
    {
        get
        {
            if (SceneLoader.instance == null)
            {
                DontDestroyOnLoad(SceneLoader.instance);
                SceneLoader.instance = new SceneLoader();
            }
            return SceneLoader.instance;
        }

    }
    public void GoToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnApplicationQuit()
    {
        SceneLoader.instance = null;
    }
}

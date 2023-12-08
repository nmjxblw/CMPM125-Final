using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToMenuScene()
    {
        Debug.Log("GoToMenuScene");
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
    public void GoToNextLeveScene(float wait)
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1, wait));
    }
    public void GoToLooseScene(float wait)
    {
        StartCoroutine(LoadScene("Loose", wait));
    }

    public void GoToWinScene(float wait)
    {
        StartCoroutine(LoadScene("Win", wait));
    }

    IEnumerator LoadScene(int index, float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(index);
    }
    IEnumerator LoadScene(string name, float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        SceneLoader.Instance = null;
    }
}

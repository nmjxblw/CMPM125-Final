using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextLevelScript : MonoBehaviour
{
    private float elapsedTime = 0f; 
    private bool finalCheckDone = false;

    public TextMeshProUGUI timerText;

    void Update()
    {
        if (!finalCheckDone)
        {
            elapsedTime += Time.deltaTime;

            if (!AreAnyGameObjectsPresent())
            {
                SceneLoader.Instance.GoToWinScene(3);
                finalCheckDone = true;
            }
            else if (elapsedTime >= 180f)
            {
                FinalLevelCheck();
                finalCheckDone = true;
            }
        }

        timerText.text = "Time: " + (180 - (int)elapsedTime).ToString();
    }

    private bool AreAnyGameObjectsPresent()
    {
        GameObject ranger = GameObject.FindGameObjectWithTag("Ranger");
        GameObject soldier = GameObject.FindGameObjectWithTag("Soldier");
        GameObject boar = GameObject.FindGameObjectWithTag("Boar");

        return ranger != null || soldier != null || boar != null;
    }

    private void FinalLevelCheck()
    {
        GameObject ranger = GameObject.FindGameObjectWithTag("Ranger");
        GameObject soldier = GameObject.FindGameObjectWithTag("Soldier");
        GameObject boar = GameObject.FindGameObjectWithTag("Boar");

        if (ranger != null || soldier != null || boar != null)
        {
            SceneLoader.Instance.GoToLooseScene(3);
        }
        else
        {
            SceneLoader.Instance.GoToWinScene(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneLoader.Instance.GoToNextLeveScene(1);
        }
    }
}

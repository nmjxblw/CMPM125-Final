using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasformUIPanel : MonoBehaviour
{
    public GameObject Panel;
    public static bool isTab = false;

    private void Start()
    {
        GameManager.Instance.Continue();
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Panel.SetActive(true);
            isTab = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Panel.SetActive(false);
            isTab = false;
        }
    }
}

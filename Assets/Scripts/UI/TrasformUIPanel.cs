using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasformUIPanel : MonoBehaviour
{
    public GameObject Panel;

    private void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Panel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Panel.SetActive(false);
        }
    }
}

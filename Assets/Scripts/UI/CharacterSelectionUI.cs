using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionUI : MonoBehaviour
{
    public List<GameObject> CharcterImages;
    private int currentSelected = 0;
    private bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        HideAll();
        CharcterImages[currentSelected].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!TrasformUIPanel.isTab) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSelected = 0;
            pressed = true;

            TransformManager.Instance.transformer(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!TransformManager.Instance.isLookingSoldier) return;
            currentSelected = 1;
            pressed = true;

            TransformManager.Instance.transformer(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!TransformManager.Instance.isLookingBoar) return;
            currentSelected = 2;
            pressed = true;

            TransformManager.Instance.transformer(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!TransformManager.Instance.isLookingRanger) return;
            currentSelected = 3;
            pressed = true;

            TransformManager.Instance.transformer(3);
        }

        if (pressed)
        {
            pressed = false;
            HideAll();
            CharcterImages[currentSelected].SetActive(true);
        }


    }

    void HideAll()
    {
        foreach (var img in CharcterImages)
        {
            img.SetActive(false);
        }
    }
}

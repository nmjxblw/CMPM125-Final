using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public List<CustomButton> buttons;
    int buttonIndex = 0;
    float lastUpdated = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnScaleButtons();
            buttons[buttonIndex].CurrentButton.onClick.Invoke();
            return;
        }

        lastUpdated += Time.deltaTime;
        if (lastUpdated < 0.25f) return;
        lastUpdated = 0;


        float v = Input.GetAxis("Vertical");
        if (v < 0)
        {
            buttonIndex++;
            if (buttonIndex >= buttons.Count) buttonIndex = 0;
            ScaleButton();
        }
        else if (v > 0)
        {
            buttonIndex--;
            if (buttonIndex < 0) buttonIndex = buttons.Count - 1;
            ScaleButton();
        }
    }

    public void ScaleButton()
    {
        UnScaleButtons();
        buttons[buttonIndex].Scale();
    }

    public void UnScaleButtons()
    {
        foreach (var b in buttons)
        {
            b.UnScale();
        }
    }
}
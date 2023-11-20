using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<CustomButton> buttons;
    Vector3 startingScale;
    public float ScaleValue = 1.1f;
    public Button CurrentButton;

    private void Start()
    {
        startingScale = transform.localScale;
        CurrentButton = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Scale();
        UnScaleButtons();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnScale();
    }

    public void Scale()
    {
        transform.localScale = new Vector3(startingScale.x * ScaleValue, startingScale.y * ScaleValue);
    }
    public void UnScale()
    {
        transform.localScale = startingScale;
    }
    void UnScaleButtons()
    {
        foreach (var b in buttons)
        {
            b.UnScale();
        }
    }
}
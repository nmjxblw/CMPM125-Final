using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Character Character;
    public TextMeshProUGUI text;
    public Slider HPSlider;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider.maxValue = TransformManager.Instance.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.value = TransformManager.Instance.currentHealth;
        text.text = $"{HPSlider.value} / {HPSlider.maxValue}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Slider VolumeSlider;

    private void Start()
    {
        VolumeSlider.value = 1;
    }

    public void SliderValueChange()
    {
        AudioListener.volume = VolumeSlider.value;
    }
}
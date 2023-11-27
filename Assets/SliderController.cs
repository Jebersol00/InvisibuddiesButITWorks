using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Text valueText;
    public Slider volumeSlider; 

    private void Start()
    {
        LoadValues();
    }

    public void VolumeSlider(float value)
    {
        valueText.text = value.ToString();
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue", 1.0f); // Default value is 1.0f
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}

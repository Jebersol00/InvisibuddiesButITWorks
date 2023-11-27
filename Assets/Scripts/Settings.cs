using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    //public float volume1;
    //public float sensitivity;
    //public void Update()
    //{
    //    PlayerStats.volume = volume1;
    //    PlayerStats.sens = sensitivity;
    //}
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerStats.volume = volume;

    }
    public void SetSense(float sens)
    {
        PlayerStats.sens = sens;
        Debug.Log("player = " + PlayerStats.sens);
        Debug.Log("sens = " + sens);
    }
}

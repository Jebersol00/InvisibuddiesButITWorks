using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    public Text resolutionText;

    private void Start()
    {
        LoadResolution();
    }

    public void SetLowResolution()
    {
        SetResolution(800, 600);
    }

    public void SetMediumResolution()
    {
        SetResolution(1280, 720);
    }

    public void SetHighResolution()
    {
        SetResolution(1920, 1080);
    }

    void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        SaveResolution(width, height);
    }

    void SaveResolution(int width, int height)
    {
        PlayerPrefs.SetInt("ScreenWidth", width);
        PlayerPrefs.SetInt("ScreenHeight", height);
        PlayerPrefs.Save();
        LoadResolution();
    }

    void LoadResolution()
    {
        int width = PlayerPrefs.GetInt("ScreenWidth", 1920);
        int height = PlayerPrefs.GetInt("ScreenHeight", 1080);

        resolutionText.text = $"{width}x{height}";
    }
}


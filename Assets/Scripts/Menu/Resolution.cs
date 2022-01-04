using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public Toggle fullscreenToggle;
    bool fullscreen = false;

    public void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
    }
    public void setResolution600()
    {
        Screen.SetResolution(800, 600, fullscreenToggle.isOn);
    }

    public void setResolution720()
    {
        Screen.SetResolution(1280, 720, fullscreenToggle.isOn);
    }

    public void setResolution1080()
    {
        Screen.SetResolution(1920, 1080, fullscreenToggle.isOn);
    }
    public void setResolution1440()
    {
        Screen.SetResolution(2560, 1440, fullscreenToggle.isOn);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSSetting : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private int fps;

    void Start()
    {
        fps = PlayerPrefs.GetInt("FPS");
        slider.value = fps;
    }
    void Update()
    {
        fps = (int)slider.value;
        if(fps == 0)
        {
            QualitySettings.vSyncCount = 1;
        }
        else if (fps == 251)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = Mathf.Clamp(fps, 1, 250);
        }
        PlayerPrefs.SetInt("FPS", fps);
        PlayerPrefs.Save();
    }
}

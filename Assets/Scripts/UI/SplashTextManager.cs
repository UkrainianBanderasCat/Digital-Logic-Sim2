using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplashTextManager : MonoBehaviour
{

    [SerializeField] private List<string> splashLines;
    [SerializeField] private TextMeshProUGUI splashTextUI;
    [SerializeField] private bool showSplashText = true;
    [SerializeField] private Toggle showSplashTextToggle;

    void Start()
    {
        showSplashText = PlayerPrefs.GetInt("ShowSplashText") == 1 ? true : false;
        showSplashTextToggle.isOn = showSplashText;
        splashTextUI.text = splashLines[Random.Range(0, splashLines.Count)];
        splashTextUI.gameObject.SetActive(showSplashText);
        SetShowSplashText(true);
    }

    void Update()
    {

    }

    public void SetShowSplashText(bool value)
    {
        showSplashText = value;
        PlayerPrefs.SetInt("ShowSplashText", showSplashText ? 1 : 0);
    }

}

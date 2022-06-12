using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextColorChanger : MonoBehaviour
{
    TextMeshProUGUI text;
    void Awake () {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeColorFromString(string colorCode) {
        Color color;
        ColorUtility.TryParseHtmlString("#" + colorCode, out color);
        text.color = color;
    }
}

using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeColor : MonoBehaviour
{
    TextMeshProUGUI text;
    void Awake () {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeColorFromString(string colorCode) {
        float r = HexToFloatNormalized(colorCode.Substring(0, 2));
        float g = HexToFloatNormalized(colorCode.Substring(2, 2));
        float b = HexToFloatNormalized(colorCode.Substring(4, 2));
        Color color = new Color(r, g, b);
        text.color = color;
    }

    private float HexToFloatNormalized(string hex) => Convert.ToInt32(hex, 16) / 255;
}

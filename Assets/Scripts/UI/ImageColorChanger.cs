using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorChanger : MonoBehaviour
{
    Image image;

    void Awake () {
        image = GetComponent<Image>();
    }

    public void ChangeColorFromString(string colorCode) {
        Color color;
        ColorUtility.TryParseHtmlString("#" + colorCode, out color);
        image.color = color;
    }
}

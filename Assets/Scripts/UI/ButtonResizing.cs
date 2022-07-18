using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonResizing : MonoBehaviour
{
    public float buttonWidthPadding = 10;
    TMP_Text text;

    void Start()
    {
        text = transform.GetComponentInChildren<TMP_Text> ();
    }

    void Update()
    {
        RectTransform buttonRect = gameObject.GetComponent<RectTransform> ();
        buttonRect.sizeDelta = new Vector2 (text.preferredWidth + buttonWidthPadding, buttonRect.sizeDelta.y);
    }
}

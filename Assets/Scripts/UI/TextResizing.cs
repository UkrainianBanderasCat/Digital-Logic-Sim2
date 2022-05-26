using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextResizing : MonoBehaviour
{
    
    public int maxSize;
    float width, height;
    public float length;
    void Start()
    {
        RectTransform rt = transform.GetComponent<RectTransform>();
        width = rt.sizeDelta.x * rt.localScale.x;
        height = rt.sizeDelta.y * rt.localScale.y;
    }

    void Update()
    {
        length = GetComponent<TMP_InputField>().text.Length;
        if (Mathf.Round(width/length) < maxSize)
            GetComponent<TMP_InputField>().pointSize = Mathf.Round(width/length);

        else
            GetComponent<TMP_InputField>().pointSize = 30;
    }
}

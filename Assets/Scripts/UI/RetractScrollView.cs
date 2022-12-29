using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RetractScrollView : MonoBehaviour
{
    public List<RectTransform> rectTransforms = new List<RectTransform>();
    public bool isOpening;
    public float width;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void Retract()
    {
        isOpening = true;
    }

    private void Update()
    {
        if(!isOpening)
        {
            return;
        }

        foreach (RectTransform rect in rectTransforms)
        {
            if(rect == rectTransform)
            {
                continue;
            }

            rect.sizeDelta = new Vector2(0f,75f);
        }

        rectTransform.sizeDelta = new Vector2(width, 75f);
        isOpening = false;
    }


}

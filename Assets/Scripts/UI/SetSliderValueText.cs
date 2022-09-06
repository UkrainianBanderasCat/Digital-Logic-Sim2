using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetSliderValueText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private List<TextPoints> textPoints;
    
    [SerializeField] private string formattedValue;

    private void Start()
    {
        updateValue();
    }

    public void updateValue()
    {
        SetValue((int)slider.value);
    }

    private void SetValue(int value)
    {
        valueText.text = formattedValue.Replace("%value%", value.ToString());
        foreach (TextPoints textPoint in textPoints)
        {
            if (textPoint.value == value)
            {
                valueText.text = textPoint.text.Replace("%value%", value.ToString());
            }
        }
    }


    [System.Serializable]
    public class TextPoints
    {
        public int value;
        public string text;
    }

}

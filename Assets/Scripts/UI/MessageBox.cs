using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[System.Serializable]
public class MessageBox
{
    public string messageBoxName;
    public string Title;
    public string Message;
    public string RedOptionText;
    public string GreenOptionText;
    public Button.ButtonClickedEvent RedOptionPressEvent;
    public Button.ButtonClickedEvent GreenOptionPressEvent;
}

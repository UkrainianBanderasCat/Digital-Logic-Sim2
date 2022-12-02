using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MessageBoxManager : MonoBehaviour
{

    [SerializeField] private List<MessageBox> messageBoxes;

    [SerializeField] private GameObject messageBoxGameObject;
    [SerializeField] private TextMeshProUGUI messageBoxTitle;
    [SerializeField] private TextMeshProUGUI messageBoxMessage;
    [SerializeField] private Button messageBoxRedOption;
    [SerializeField] private Button messageBoxGreenOption;
    [SerializeField] private TextMeshProUGUI messageBoxRedOptionText;
    [SerializeField] private TextMeshProUGUI messageBoxGreenOptionText;

    public void ShowMessageBox(string messageBoxName)
    {
        foreach(MessageBox msgBox in messageBoxes)
        {
            if(msgBox.messageBoxName == messageBoxName)
            {
                messageBoxGameObject.SetActive(true);
                InitializeMessageBox(msgBox.Title, msgBox.Message, msgBox.RedOptionText, msgBox.GreenOptionText, msgBox.RedOptionPressEvent, msgBox.GreenOptionPressEvent);
            }
        }
    }

    public void InitializeMessageBox(string title, string message, string redOptionText, string greenOptionText, Button.ButtonClickedEvent redOptionPressEvent, Button.ButtonClickedEvent greenOptionPressEvent)
    {
        messageBoxTitle.text = title;
        messageBoxMessage.text = message;
        messageBoxRedOptionText.text = redOptionText;
        messageBoxGreenOptionText.text = greenOptionText;
        messageBoxRedOption.onClick = redOptionPressEvent;
        messageBoxGreenOption.onClick = greenOptionPressEvent;
    }

    public void Discard()
    {
        messageBoxGameObject.GetComponent<Animator>().SetTrigger("Pop Down");
    }

}

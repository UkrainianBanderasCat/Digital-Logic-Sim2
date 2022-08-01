using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditManager : MonoBehaviour
{
    [SerializeField] private List<Credits> credits;

    private string creditsText;

    [SerializeField] private TextMeshProUGUI creditsTextUI;

    private void Awake()
    {
        foreach(Credits credit in credits)
        {
            creditsText += "<line-height=50%>\n</line-height><color=#D9D9D9><size=150%>" + credit.creditHeader + "</size></color><line-height=130%>\n</line-height>";
            foreach(string name in credit.names)
            {
                creditsText += name + "\n";
            }
        }
    }

    private void Start()
    {
        creditsTextUI.text = creditsText;
    }

}

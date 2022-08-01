using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TernarySignal : TernaryChipSignal
{
    protected override void Start () {
		base.Start ();
		SetCol ();
        
        if (side == Side.Left)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);

        if (side == Side.Right)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
	}

	public void ToggleActive () {
        if (pinType == PinType.Input)
        {
            if (currentState == 2) {
                currentState = 0;
            } else {
                currentState += 1;
            }
		    
		    SetCol ();
        }
	}

	public void SendSignal (int signal) {
        if (pinType == PinType.Input)
        {
		    currentState = signal;
		    outputPins[0].ReceiveSignal (signal);
                    SetCol ();
        }
	}

	public void SendSignal () {
        if (pinType == PinType.Input)
        {
		gameObject.tag = "Zoom";
		outputPins[0].ReceiveSignal (currentState);
        }
	}

    public override void ReceiveInputSignal (Pin inputPin) {
        if (pinType == PinType.Output)
        {
		currentState = inputPin.State;
		SetDisplayState (inputPin.State);
        }
	}

	void SetCol () 
    {
		if (currentState == 2) {
				indicatorRenderer.material.color = palette.onCol;
			} else if (currentState == 1) { 
				indicatorRenderer.material.color = palette.midCol;
			} else {
				indicatorRenderer.material.color = palette.offCol;
			}
	}

	public override void UpdateSignalName (string newName) {
        if (pinType == PinType.Input)
        {
		base.UpdateSignalName (newName);
		outputPins[0].pinName = newName;
        }

        else 
        {
            base.UpdateSignalName (newName);
		inputPins[0].pinName = newName;
        }
	}

	void OnMouseDown () {
        if (pinType == PinType.Input)
        {
		Debug.Log("Stop");
		ToggleActive();
        }
	}

    void Update()
    {
        base.Start ();

        if (pinType == PinType.Input)
        {
		    outputPins[0].pinType = Pin.PinType.ChipOutput;
        }

        if (pinType == PinType.Output) 
        {
		    inputPins[0].pinType = Pin.PinType.ChipInput;
        }
    }
}
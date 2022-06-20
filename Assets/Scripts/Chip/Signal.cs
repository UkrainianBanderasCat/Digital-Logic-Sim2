using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signal : ChipSignal
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
		    currentState = 1 - currentState;
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
		indicatorRenderer.material.color = (currentState == 1) ? new Color(0.9245283f, 0.1351905f, 0.2190198f) : new Color(0.1254902f, 0.1411765f, 0.1803922f);
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
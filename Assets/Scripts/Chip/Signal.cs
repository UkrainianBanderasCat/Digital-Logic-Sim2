using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : ChipSignal
{
    public enum PinType { Output, Input }

    public PinType pinType;

    protected override void Start () {
		base.Start ();

        //if (pinType == PinType.Input)
		    SetCol ();
        
        //if (pinType == PinType.Output)
            SetDisplayState (0);
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

	void SetCol () {
        if (pinType == PinType.Input)
        {
		    SetDisplayState (currentState);
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
		    ToggleActive ();
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
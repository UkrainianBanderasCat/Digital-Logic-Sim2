using UnityEngine;

public class AndGate : BuiltinChip {

	protected override void Awake () {
		base.Awake ();
	}

	protected override void ProcessOutput (int[] input) {
		int outputSignal = input[0] & input[1];
		//outputSignal = 1 - outputSignal;
		outputPins[0].ReceiveSignal (outputSignal);
	}

}
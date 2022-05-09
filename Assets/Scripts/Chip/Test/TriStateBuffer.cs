using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriStateBuffer : Chip {

	protected override void Awake () {
		base.Awake ();
	}

	protected override void ProcessOutput (int[] input) {
		int data = input[0];
		int enable = input[1];

		if (enable == 1) {
			//Debug.Log (data + "  " + enable + ":  " + data);
			outputPins[0].ReceiveSignal (data);
		} else {
			//Debug.Log (data + "  " + enable + ":  -1");
			outputPins[0].ReceiveSignal (-1);
		}

	}

}
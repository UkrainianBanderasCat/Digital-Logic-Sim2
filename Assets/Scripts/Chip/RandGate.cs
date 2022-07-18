using UnityEngine;
using System.Collections;

public class RandGate : BuiltinChip
{
    public bool output;
    public int signal;

    protected override void ProcessOutput (int[] input) {
		if(input[0] == 1 && !output)
        {
            output = true;
            signal = Random.Range(0, 2);
            outputPins[0].ReceiveSignal (signal);
        }

        if (input[0] == 0)
            output = false;

        outputPins[0].ReceiveSignal (signal);
	}
}

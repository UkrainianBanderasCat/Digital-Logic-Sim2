public class TernaryAndGate : BuiltinChip {

	protected override void ProcessOutput (int[] input) {

        int outputSignal = 0;

        if (input[0] < input[1]) {
            outputSignal = input[0];
        } else {
            outputSignal = input[1];
        }

		outputPins[0].ReceiveSignal (outputSignal);
	}

}
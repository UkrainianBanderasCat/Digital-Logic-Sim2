public class TernaryDownGate : BuiltinChip {

	protected override void ProcessOutput (int[] input) {

        int outputSignal = 0;

		if (input[0] == 0) {
            outputSignal = 2;
        } else {
            outputSignal = input[0] - 1;
        }
		outputPins[0].ReceiveSignal (outputSignal);
	}
}
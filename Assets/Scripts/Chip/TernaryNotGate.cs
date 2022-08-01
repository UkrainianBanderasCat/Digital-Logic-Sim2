public class TernaryNotGate : BuiltinChip {

	protected override void ProcessOutput (int[] input) {

		int outputSignal = ((input[0]-1)*-1)+1;
		outputPins[0].ReceiveSignal (outputSignal);
	}
}
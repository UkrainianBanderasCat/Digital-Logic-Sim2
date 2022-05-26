public class NotGate : BuiltinChip {

	protected override void Awake () {
		base.Awake ();
	}

	protected override void ProcessOutput (int[] input) {
		int outputSignal = 1 - input[0];
		outputPins[0].ReceiveSignal (outputSignal);
	}
}
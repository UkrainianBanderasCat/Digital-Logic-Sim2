public class Clock : BuiltinChip {

    protected override void Awake () {
        Simulation.onClockCycle += UpdateClockSignals;
        base.Awake();
    }

    void UpdateClockSignals (int signal) {
        outputPins[0].ReceiveSignal (signal);
    }
}
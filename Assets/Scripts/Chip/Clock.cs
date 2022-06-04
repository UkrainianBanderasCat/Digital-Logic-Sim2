public class Clock : BuiltinChip {

    protected override void Awake () {
        Simulation.onClockCycle += ClockCycle;
        base.Awake();
    }

    void ClockCycle (int state) {
        outputPins[0].ReceiveSignal (state);
    }

}
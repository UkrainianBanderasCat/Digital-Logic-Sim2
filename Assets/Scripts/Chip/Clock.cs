using UnityEngine.UI;
using UnityEngine;

public class Clock : BuiltinChip {
    protected override void Awake () {
        Simulation.onClockCycle += FlipOutputSignal;
        base.Awake();
    }

    void FlipOutputSignal (int signal) {
        outputPins[0].ReceiveSignal (signal);
    }

}
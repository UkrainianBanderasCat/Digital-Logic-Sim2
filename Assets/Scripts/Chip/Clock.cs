using UnityEngine;

public class Clock : BuiltinChip {

    public float clockCycle = 1;
    int currentOutputSignal = 0;
    float cycleTimeLeft;

    protected override void Awake () {
        cycleTimeLeft = clockCycle;
        Simulation.onDebugClockCycle += DebugClockCycle;
        base.Awake();
    }

    void Update () {
        if (Simulation.debugMode) {
            return;
        }

        cycleTimeLeft -= Time.deltaTime;
        if (cycleTimeLeft > 0) {
            return;
        }
        FlipOutputSignal ();
        cycleTimeLeft = clockCycle;
    }

    void DebugClockCycle () {
        FlipOutputSignal ();
    }

    void FlipOutputSignal () {
        currentOutputSignal = 1 - currentOutputSignal;
        outputPins[0].ReceiveSignal (currentOutputSignal);
    }

}
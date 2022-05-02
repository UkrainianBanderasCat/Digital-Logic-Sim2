using UnityEngine;

public class Clock : BuiltinChip {

    public float clockCycle = 1;
    public int currentOutputSignal = 0;
    float cycleTimeLeft;

    protected override void Awake () {
        cycleTimeLeft = clockCycle;
        base.Awake();
    }

    void Update () {
        cycleTimeLeft -= Time.deltaTime;
        if (cycleTimeLeft > 0) {
            return;
        }
        FlipOutputSignal ();
        cycleTimeLeft = clockCycle;
    }

    void FlipOutputSignal () {
        currentOutputSignal = 1 - currentOutputSignal;
        outputPins[0].ReceiveSignal (currentOutputSignal);
    }

}
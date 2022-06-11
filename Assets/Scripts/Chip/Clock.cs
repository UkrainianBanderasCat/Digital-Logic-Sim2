using UnityEngine.UI;
using UnityEngine;

public class Clock : BuiltinChip {

    public float clockCycle = 1;
    int currentOutputSignal = 0;
    float cycleTimeLeft;
    public Slider[] clockSpeedController;

    protected override void Awake () {
        cycleTimeLeft = clockCycle;
        base.Awake();
    }

    void Update () {
        clockSpeedController = GameObject.Find("UI Manager").transform.GetChild(3).gameObject.GetComponentsInChildren<Slider>(true);
        clockCycle = ( clockSpeedController[0].value * 10 ) / 5;

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
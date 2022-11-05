using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

	public static event System.Action<int> onClockCycle;
	public static event System.Action onStoreInputDebug;
	public static event System.Action onDebugStep;

	public static int simulationFrame { get; private set; }
	public static bool debugMode { get; private set; }

	static Simulation instance;
	InputSignal[] inputSignals;
	ChipEditor chipEditor;

	public float minStepTime = 0.075f;
	float lastStepTime;
	public float clockCycleDuration = 0.005f;
	bool clockEnabled = true;
    public float cycleTimeLeft;
	public int currentClockState = 0;

	void Awake () {
		simulationFrame = 0;
        cycleTimeLeft = clockCycleDuration;
	}

	void Update ()
    {
        if (Time.fixedTime - lastStepTime > minStepTime)
        {
            lastStepTime = Time.deltaTime;
            StepSimulation();
        }
		
		UpdateClocks();
		onClockCycle?.Invoke(currentClockState);
    }

    private void UpdateClocks()
    {
        if (Simulation.debugMode)
        {
            return;
        }
        // if (!clockEnabled)
        // {
        //     return;
        // }
		
        cycleTimeLeft -= Time.deltaTime;
        if (cycleTimeLeft < 0)
        {
            onClockCycle?.Invoke(currentClockState);
            currentClockState = 1 - currentClockState;
            cycleTimeLeft = clockCycleDuration;
        }
    }

    public void SetSignalSpeed(float speed) {
		minStepTime = speed;
	}

	public void SetClockSpeed(float speed) {
		clockEnabled = true;
		clockCycleDuration = speed;
	}

	public void SetDebugMode (bool debug) {
		debugMode = debug;
	}

	public void DebugStep () {
		onStoreInputDebug?.Invoke ();
		onDebugStep?.Invoke ();
	}

	public void DebugClockCycle () {
		currentClockState = 1 - currentClockState;
		onClockCycle?.Invoke (currentClockState);
	}

	void StepSimulation () {
		simulationFrame++;
		RefreshChipEditorReference ();

		if (!debugMode)
		{	
			// Clear output signals
			List<ChipSignal> outputSignals = new List<ChipSignal>();

			for (int i = 0; i < chipEditor.outputsEditor.signals.Count; i++)
			{
				outputSignals.Add(chipEditor.outputsEditor.signals[i]);
			}

			for (int i = 0; i < outputSignals.Count; i++) {
				//if (outputSignals[i].inputPins[0].pinType == Pin.PinType.ChipInput)
					outputSignals[i].SetDisplayState (0);
			}
		}

		// Init chips
		var allChips = chipEditor.chipInteraction.allChips;
		for (int i = 0; i < allChips.Count; i++) {
			allChips[i].InitSimulationFrame ();
		}

		// Process inputs
		List<ChipSignal> inputSignals = new List<ChipSignal>();

		for (int i = 0; i < chipEditor.inputsEditor.signals.Count; i++)
		{
			//if (chipEditor.inputsEditor.signals[i].outputPins[0].pinType == Pin.PinType.ChipOutput)
			//{
				inputSignals.Add(chipEditor.inputsEditor.signals[i]);
			//}
		}

		// for (int i = 0; i < chipEditor.outputsEditor.signals.Count; i++)
		// {
		// 	//if (chipEditor.outputsEditor.signals[i].outputPins[0].pinType == Pin.PinType.ChipOutput)
		// 	//{
		// 		inputSignals.Add(chipEditor.outputsEditor.signals[i]);
		// 	//}
		// }
		// Tell all signal generators to send their signal out
		for (int i = 0; i < inputSignals.Count; i++) {
			((InputSignal) inputSignals[i]).SendSignal();
		}

	}

	void RefreshChipEditorReference () {
		if (chipEditor == null) {
			chipEditor = FindObjectOfType<ChipEditor> ();
		}
	}

	static Simulation Instance {
		get {
			if (!instance) {
				instance = FindObjectOfType<Simulation> ();
			}
			return instance;
		}
	}
}
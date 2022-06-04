using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

	public static event System.Action<int> onClockCycle;
	public static event System.Action onUpdateClockSignals;
	public static event System.Action onStoreInputDebug;
	public static event System.Action onDebugStep;

	public static int simulationFrame { get; private set; }
	public static bool debugMode { get; private set; }

	static Simulation instance;
	InputSignal[] inputSignals;
	ChipEditor chipEditor;

	public float minStepTime = 0.075f;
	float lastStepTime;
	float clockCycleSpeed = 0.6f;
    float cycleTimeLeft;
	int currentClockState = 0;

	void Awake () {
		simulationFrame = 0;
        cycleTimeLeft = clockCycleSpeed;
	}

	void Update () {
		if (Time.time - lastStepTime > minStepTime) {
			lastStepTime = Time.time;
			StepSimulation ();
		}
		
		if (Simulation.debugMode) {
            return;
        }

        cycleTimeLeft -= Time.deltaTime;
        if (cycleTimeLeft > 0) {
            return;
        }
		currentClockState = 1 - currentClockState;
        onClockCycle?.Invoke (currentClockState);
		onUpdateClockSignals?.Invoke ();
        cycleTimeLeft = clockCycleSpeed;
	}

	public void SetSignalSpeed(float speed) {
		minStepTime = speed;
	}

	public void SetClockSpeed(float speed) {
		clockCycleSpeed = speed;
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
		onUpdateClockSignals?.Invoke ();
	}

	void StepSimulation () {
		simulationFrame++;
		RefreshChipEditorReference ();

		if (!debugMode)
		{	
			// Clear output signals
			List<ChipSignal> outputSignals = chipEditor.outputsEditor.signals;
			for (int i = 0; i < outputSignals.Count; i++) {
				outputSignals[i].SetDisplayState (0);
			}
		}

		// Init chips
		var allChips = chipEditor.chipInteraction.allChips;
		for (int i = 0; i < allChips.Count; i++) {
			allChips[i].InitSimulationFrame ();
		}

		// Process inputs
		List<ChipSignal> inputSignals = chipEditor.inputsEditor.signals;
		// Tell all signal generators to send their signal out
		for (int i = 0; i < inputSignals.Count; i++) {
			((InputSignal) inputSignals[i]).SendSignal ();
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
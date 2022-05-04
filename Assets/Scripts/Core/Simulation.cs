using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

	public static int simulationFrame { get; private set; }
	public static bool debugMode { get; private set; }
	public static event System.Action onStoreInputDebug;
	public static event System.Action onDebugStep;
	public static event System.Action onDebugClockCycle;

	static Simulation instance;
	InputSignal[] inputSignals;
	ChipEditor chipEditor;

	public float minStepTime = 0.075f;
	float lastStepTime;

	void Awake () {
		simulationFrame = 0;
	}

	void Update () {
		if (Time.time - lastStepTime > minStepTime) {
			lastStepTime = Time.time;
			StepSimulation ();
		}
	}

	public void SetDebugMode (bool debug) {
		debugMode = debug;
	}

	public void DebugStep () {
		onStoreInputDebug?.Invoke ();
		onDebugStep?.Invoke ();
	}

	public void DebugClockCycle () {
		onDebugClockCycle?.Invoke ();
	}

	void StepSimulation () {
		simulationFrame++;
		RefreshChipEditorReference ();

		// Clear output signals
		if (!debugMode)
		{	
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
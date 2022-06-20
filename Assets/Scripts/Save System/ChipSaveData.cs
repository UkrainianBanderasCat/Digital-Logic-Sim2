using System.Collections.Generic;
using UnityEngine;

public class ChipSaveData {

	public string chipName;
	public Color chipColour;
	public Color chipNameColour;
	public int creationIndex;

	// All chips used as components in this new chip (including input and output signals)
	public Chip[] componentChips;
	// All wires in the chip (in case saving of wire layout is desired)
	public Wire[] wires;

	public ChipSaveData () {

	}

	public ChipSaveData (ChipEditor chipEditor, ChipSignal inputSignalPf, Transform signalHolder) {
		List<Chip> componentChipList = new List<Chip> ();

		var sortedInputs = chipEditor.inputsEditor.signals;
		sortedInputs.Sort ((a, b) => b.transform.position.y.CompareTo (a.transform.position.y));

		List<Chip> clocks = new List<Chip>();
		List<Pin> clockControlledPins = new List<Pin>();
		foreach (Chip chip in chipEditor.chipInteraction.allChips) {
			if (chip is Clock) {
				clocks.Add(chip);
				clockControlledPins.AddRange (chip.outputPins[0].childPins);
				MonoBehaviour.Destroy (chip.gameObject);
			}
		}
		if (clocks.Count > 0) {
			ChipSignal clockInputSignal = MonoBehaviour.Instantiate (inputSignalPf, signalHolder);
			clockInputSignal.UpdateSignalName("CLOCK");
			foreach (Pin pin in clockControlledPins) {
				Pin.MakeConnection (clockInputSignal.outputPins[0], pin);
			}
			sortedInputs.Add (clockInputSignal);
		}

		var sortedOutputs = chipEditor.outputsEditor.signals;
		sortedOutputs.Sort ((a, b) => b.transform.position.y.CompareTo (a.transform.position.y));

		componentChipList.AddRange (sortedInputs);
		componentChipList.AddRange (sortedOutputs);

		componentChipList.AddRange (chipEditor.chipInteraction.allChips);
		componentChips = componentChipList.ToArray ();

		wires = chipEditor.pinAndWireInteraction.allWires.ToArray ();
		chipName = chipEditor.chipName;
		chipColour = chipEditor.chipColour;
		chipNameColour = chipEditor.chipNameColour;
		creationIndex = chipEditor.creationIndex;
	}

	public int ComponentChipIndex (Chip componentChip) {
		return System.Array.IndexOf (componentChips, componentChip);
	}

}
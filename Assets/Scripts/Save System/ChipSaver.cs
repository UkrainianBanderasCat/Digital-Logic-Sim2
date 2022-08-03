using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class ChipSaver {

	const bool usePrettyPrint = true;

	public static void Save (ChipEditor chipEditor, ChipSignal inputSignalPf, Transform signalHolder, string chipName = null) {
		ChipSaveData chipSaveData = new ChipSaveData (chipEditor, inputSignalPf, signalHolder, chipName);

		if (chipName == null) { chipName = chipEditor.chipName; }
		// Generate new chip save string
		var compositeChip = new SavedChip (chipSaveData);
		string saveString = JsonUtility.ToJson (compositeChip, usePrettyPrint);

		// Generate save string for wire layout
		var wiringSystem = new SavedWireLayout (chipSaveData);
		string wiringSaveString = JsonUtility.ToJson (wiringSystem, usePrettyPrint);

		// Write to file
		string savePath = SaveSystem.GetPathToSaveFile (chipName);
		using (StreamWriter writer = new StreamWriter (savePath)) {
			writer.Write (saveString);
		}

		string wireLayoutSavePath = SaveSystem.GetPathToWireSaveFile (chipName);
		using (StreamWriter writer = new StreamWriter (wireLayoutSavePath)) {
			writer.Write (wiringSaveString);
		}

		// Write to Global Save File
		string globalSavePath = SaveSystem.GetPathToGlobalSaveFile(chipName);
		using (StreamWriter writer = new StreamWriter(globalSavePath))
		{
			writer.Write(saveString);
		}

		string globalWireLayoutSavePath = SaveSystem.GetPathToGlobalWireSaveFile(chipName);
		using (StreamWriter writer = new StreamWriter(globalWireLayoutSavePath))
		{
			writer.Write(wiringSaveString);
		}
	}

}
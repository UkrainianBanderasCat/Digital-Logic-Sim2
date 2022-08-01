using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TernaryChipSignal : Chip
{
  
	public enum Side { Left, Right }
	public enum PinType { Input, Output }

	public int currentState;

	public Palette palette;
	public MeshRenderer indicatorRenderer;
	public MeshRenderer pinRenderer;
	public MeshRenderer wireRenderer;

	int groupID = -1;
	public bool displayGroupDecimalValue { get; set; } = false;
	public bool useTwosComplement { get; set; } = true;

	[HideInInspector]
	public string signalName;
	public bool interactable = true;

	public Side side;
    public PinType pinType;

	public virtual void SetInteractable (bool interactable) {
		this.interactable = interactable;

		if (!interactable) {
			indicatorRenderer.material.color = palette.nonInteractableCol;
			pinRenderer.material.color = palette.nonInteractableCol;
			wireRenderer.material.color = palette.nonInteractableCol;
		}
	}

	public void SetDisplayState (int state) {

		if (indicatorRenderer && interactable) {
			if (state == 2) {
				indicatorRenderer.material.color = palette.onCol;
			} else if (state == 1) { 
				indicatorRenderer.material.color = palette.midCol;
			} else {
				indicatorRenderer.material.color = palette.offCol;
			}
		}
	}

	public static bool InSameGroup (TernaryChipSignal signalA, TernaryChipSignal signalB) {
		return (signalA.groupID == signalB.groupID) && (signalA.groupID != -1);
	}

	public int GroupID {
		get {
			return groupID;
		}
		set {
			groupID = value;
		}
	}

	public virtual void UpdateSignalName (string newName) {
		signalName = newName;
	}
}

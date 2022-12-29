using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipBarUI : MonoBehaviour {
	public RectTransform bar;
	public Transform buttonHolder;
	public Transform displayButtonHolder;
	public Transform utilityButtonHolder;
	public CustomButton buttonPrefab;
	public float buttonSpacing = 15f;
	public float buttonWidthPadding = 10;
	float rightmostButtonEdgeX;
	Manager manager;
	public List<string> hideList;
	public Scrollbar horizontalScroll;

	void Awake () {
		manager = FindObjectOfType<Manager> ();
		manager.customChipCreated += AddChipButton;
		for (int i = 0; i < manager.builtinChips.Length; i++) {
			AddChipButton (manager.builtinChips[i], buttonHolder);
		}
        for (int i = 0; i < manager.displayChips.Length; i++)
        {
            AddChipButton(manager.displayChips[i], displayButtonHolder);
        }
        for (int i = 0; i < manager.utilityChips.Length; i++)
        {
            AddChipButton(manager.utilityChips[i], utilityButtonHolder);
        }




        Canvas.ForceUpdateCanvases ();
	}

	void LateUpdate () {
		UpdateBarPos ();
	}

	void UpdateBarPos () {
		float barPosY = (horizontalScroll.gameObject.activeSelf) ? 0-500 : 0-500;
		bar.localPosition = new Vector3 (45, barPosY, 0);
	}

	void AddChipButton (Chip chip, Transform holder) {
		if (hideList.Contains (chip.chipName)) {
			//Debug.Log("Hiding")
			return;
		}
		CustomButton button = Instantiate (buttonPrefab);
		button.gameObject.name = chip.chipName;
		if (button.gameObject.name == "")
		{
			button.gameObject.SetActive(false);
		}
		// Set button text
		var buttonTextUI = button.GetComponentInChildren<TMP_Text> ();
		buttonTextUI.text = chip.chipName;

		// Set button size
		var buttonRect = button.GetComponent<RectTransform> ();
		buttonRect.sizeDelta = new Vector2 (buttonTextUI.preferredWidth + buttonWidthPadding, buttonRect.sizeDelta.y);

		// Set button position
		buttonRect.SetParent (holder, false);
		//buttonRect.localPosition = new Vector3 (rightmostButtonEdgeX + buttonSpacing + buttonRect.sizeDelta.x / 2f, 0, 0);
		rightmostButtonEdgeX = buttonRect.localPosition.x + buttonRect.sizeDelta.x / 2f;

		// Set button event
		//button.onClick.AddListener (() => manager.SpawnChip (chip));
		button.onPointerDown += (() => manager.SpawnChip (chip));

		GameObject.Find("UI Manager").transform.GetChild(0).gameObject.GetComponent<CreateMenu>().chipNames.Add(chip.chipName);
	}

}


﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public TMP_InputField projectNameField;
	public Button confirmProjectButton;
	public Toggle fullscreenToggle;
	public Toggle showFPSToggle;
	public ShowFPS showFPS;
	public SceneLoader sceneLoader;

	void Awake () {
		fullscreenToggle.onValueChanged.AddListener (SetFullScreen);
		showFPSToggle.isOn = PlayerPrefs.GetInt("showFPS") == 0 ? false : true;
	}

	void LateUpdate () {
		confirmProjectButton.interactable = projectNameField.text.Trim ().Length > 0;
		if (fullscreenToggle.isOn != Screen.fullScreen) {
			fullscreenToggle.SetIsOnWithoutNotify (Screen.fullScreen);
		}
	}

	public void StartNewProject () {
		string projectName = projectNameField.text;
		if(projectName.ToLower() == "global")
        {
			projectName = "";
			projectNameField.text = "";
			return;
        }
		SaveSystem.SetActiveProject (projectName);
		sceneLoader.LoadScene("Chip Design");
	}

	public void SetResolution16x9 (int width) {
		Screen.SetResolution (width, Mathf.RoundToInt (width * (9 / 16f)), Screen.fullScreenMode);
	}

	public void SetFullScreen (bool fullscreenOn) {
		//Screen.fullScreen = fullscreenOn;
		var nativeRes = Screen.resolutions[Screen.resolutions.Length - 1];
		if (fullscreenOn) {
			Screen.SetResolution (nativeRes.width, nativeRes.height, FullScreenMode.FullScreenWindow);
		} else {
			float windowedScale = 0.75f;
			int x = nativeRes.width / 16;
			int y = nativeRes.height / 9;
			int m = (int) (Mathf.Min (x, y) * windowedScale);
			Screen.SetResolution (16 * m, 9 * m, FullScreenMode.Windowed);
		}

	}
	public void SetFPSDisplay(bool display)
	{
		showFPS.showFPS = display;
		PlayerPrefs.SetInt("showFPS", display ? 1 : 0);
	}

	public void Quit () {
		Application.Quit ();
	}
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Button button;
	public TMPro.TMP_Text buttonText;
	public Color normalCol = Color.white;
	public Color nonInteractableCol = Color.grey;
	public Color highlightedCol = Color.white;
	bool highlighted;

	string directoryPath = "";
    public FileInfo[] info;
	AudioSource audioSource;
	List<AudioClip> clips = new List<AudioClip>();
	List<string> files = new List<string>();

	new string name;

	public ChipDelete _chipDelete;

	void Start () {
		if (GameObject.FindWithTag("Manager") != null)
			_chipDelete = GameObject.FindWithTag("Manager").GetComponent<ChipDelete>();
		
		name = gameObject.name;

		// directoryPath = Application.dataPath + "/StreamingAssets/Sounds/Click";
        
		// string[] f;
        // f = Directory.GetFiles(directoryPath);

		// for (int i = 0; i < f.Length; i++)
        // {
        //     if (f[i].EndsWith(".wav"))
        //     {
        //         files.Add(f[i]);
        //         clips.Add(new WWW(f[i]).GetAudioClip(false, true, AudioType.WAV));
        //     }
        // }

		// if (gameObject.GetComponent<AudioSource>() != null) 
		// 	audioSource = gameObject.GetComponent<AudioSource>();
		// else	
		// 	audioSource = gameObject.AddComponent<AudioSource>();

		// Button btn = button.GetComponent<Button>();
		// btn.onClick.AddListener(PlaySound);
	}

	void Update () {
		if (buttonText == null || button == null || nonInteractableCol == null) return;
		Color col = (highlighted) ? highlightedCol : normalCol;
		buttonText.color = (button.interactable) ? col : nonInteractableCol;
		if (Input.GetKeyDown(KeyCode.Delete) && highlighted && _chipDelete != null)
		{
			_chipDelete.ConfirmDelete(name);
		}
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (button.interactable) {
			highlighted = true;
		}
	}

	public void OnPointerExit (PointerEventData eventData) {
		highlighted = false;
	}

	void OnEnable () {
		highlighted = false;
	}

	void OnValidate () {
		if (button == null) {
			button = GetComponent<Button> ();
		}
		if (buttonText == null) {
            buttonText = transform.GetComponentInChildren<TMPro.TMP_Text>();
        }
	}

	// void PlaySound()
	// {
	// 	audioSource.clip = clips[Random.Range(0, clips.Count)];
	// 	audioSource.Play();	
	// }
}
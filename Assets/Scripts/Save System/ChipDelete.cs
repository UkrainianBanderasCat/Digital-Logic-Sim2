using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ChipDelete : MonoBehaviour
{
    new string name;
	const string fileExtension = ".txt";
    Manager _manager;
    public GameObject deleteConfirmation;
    public TextMeshProUGUI confirmText;

    void Start() {
        _manager = GameObject.FindWithTag("Manager").GetComponent<Manager>();
    }

    public void Delete(string _name) {
        deleteConfirmation.SetActive(true);
        confirmText.text = "Do you want to delete chip:" + "\n" + _name + "\n" + "If it used in other chips," + "\n" + "it can break this project";
        name = _name;
    }

    public void ConfirmDelete()
    {
        string deletePath = SaveSystem.GetPathToSaveFile(name);
        Debug.Log(deletePath);
        if (File.Exists(deletePath)) {
            File.Delete(deletePath);
            _manager.RefreshAll();
        }

        else { Debug.Log("File doesn't exist"); }
    }
}

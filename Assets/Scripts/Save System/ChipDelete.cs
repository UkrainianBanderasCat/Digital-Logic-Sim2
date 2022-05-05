using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChipDelete : MonoBehaviour
{
    static string activeProjectName = "Untitled";
	const string fileExtension = ".txt";
    Manager _manager;

    void Start() {
        _manager = GameObject.FindWithTag("Manager").GetComponent<Manager>();
    }

    public void Delete(string name) {
        string deletePath = SaveSystem.GetPathToSaveFile(name);
        if (File.Exists(deletePath)) {
            File.Delete(deletePath);
            _manager.RefreshAll();
        }

        else { Debug.Log("File doesn't exist"); }
    }
}

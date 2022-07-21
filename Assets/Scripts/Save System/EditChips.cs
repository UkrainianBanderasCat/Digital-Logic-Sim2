using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
using SimpleFileBrowser;


public class EditChips : MonoBehaviour
{
    [Header ("References")]
    public Transform implementationHolder;
    public GameObject manager;
    public ChipSignal InputSignalPrefab;
    public ChipSignal OutputSignalPrefab;
    GameObject InputBar;
    GameObject OutputBar;


    Vector2 chipPos;
    private IEnumerator coroutine;

    public void OpenFileBrowser()
    {
        FileBrowser.SetFilters(false, ".txt");
        FileBrowser.ShowLoadDialog((path) => { AllMethod(path); }, null, FileBrowser.PickMode.Files, false, SaveSystem.CurrentSaveProfileDirectoryPath, null, "Load Chip Save File", "Load") ;

    }


    void AllMethod(string[] path)
    {
        DisplayChips(path);
    }

    void DisplayChips(string[] chipPath)
    {
        
        SavedChip savedChip;
        using (StreamReader reader = new StreamReader (chipPath[0])) {
				string chipSaveString = reader.ReadToEnd ();
				savedChip = JsonUtility.FromJson<SavedChip> (chipSaveString);
			}
        foreach(SavedComponentChip componentChip in savedChip.savedComponentChips)
        {
            
            string chipName = componentChip.chipName;
            if((chipName != "SIGNAL IN" )&&( chipName != "SIGNAL OUT") )
            {
                
                if (IsBuiltInChipName(chipName))
                {
                    for (int i = 0; i < manager.GetComponent<Manager>().builtinChips.Length; i++)
                    {
                        if(chipName == manager.GetComponent<Manager>().builtinChips[i].name)
                        {
                            chipPos = new Vector2((float)componentChip.posX, (float)componentChip.posY);
                            GameObject.Instantiate(manager.GetComponent<Manager>().builtinChips[i], chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform);
                        }
                    }

                    
                }


                else
                {
                    List<GameObject> inactiveGameObjects = new List<GameObject>();

                    foreach (GameObject gameObject in FindInActiveObjectsByName(chipName))
                    {
                        if (!gameObject.activeSelf)
                        {
                            inactiveGameObjects.Add(gameObject);
                            gameObject.SetActive(true);
                        }
                    }

                    GameObject original = GameObject.Find("/Manager/" + chipName);

                    chipPos = new Vector2((float)componentChip.posX, (float)componentChip.posY);
                    GameObject.Instantiate(original, chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform);

                    foreach (GameObject gameObject in inactiveGameObjects)
                    {
                        gameObject.SetActive(false);
                    }
                }
                
                
            }


            else
            {
                InputBar = GameObject.Find("Input Bar");
                OutputBar = GameObject.Find("Output Bar");
                if (chipName == "SIGNAL IN")
                {
                    chipPos = new Vector2((float)componentChip.posX, (float)componentChip.posY);
                    ChipSignal spawnedSignal = Instantiate (InputSignalPrefab, chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform.Find("Inputs"));
                    InputBar.GetComponent<ChipInterfaceEditor>().signals.Add(spawnedSignal);
                    spawnedSignal.side = ChipSignal.Side.Left;

                }

                if (chipName == "SIGNAL OUT")
                {
                    chipPos = new Vector2((float)componentChip.posX, (float)componentChip.posY);
                    ChipSignal spawnedSignal = Instantiate(OutputSignalPrefab, chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform.Find("Inputs"));
                    OutputBar.GetComponent<ChipInterfaceEditor>().signals.Add(spawnedSignal);
                    spawnedSignal.side = ChipSignal.Side.Right;

                }

            }

            

        }
    }


    void DisplayWires(string[] path)
    {
        SavedWireLayout savedWireLayout;
        string wirePath = SaveSystem.GetPathToWireSaveFile(path[0]);
        using (StreamReader reader = new StreamReader(wirePath))
            {
                string wireSaveString = reader.ReadToEnd();
                savedWireLayout = JsonUtility.FromJson<SavedWireLayout>(wireSaveString);
            }
        foreach (SavedWire wire in savedWireLayout.serializableWires)
        {

        }


    }






    //Piece of code I found on stackoverflow by https://stackoverflow.com/users/3785314/programmer
    GameObject[] FindInActiveObjectsByName(string name)
    {
        List<GameObject> validTransforms = new List<GameObject>();
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].gameObject.name == name)
                {
                    validTransforms.Add(objs[i].gameObject);
                }
            }
        }
        return validTransforms.ToArray();
    }

    bool IsBuiltInChipName(string chipName)
    {
        string[] builInNames = {"AND", "NOT", "CLOCK", "SCREEN", "7SEG DISP", "SYMB", "KEY", "RAND" };
        foreach (string builInName in builInNames)
        {
            if (chipName == builInName)
            {
                return true;
            }
        }
        return false;
    }
}
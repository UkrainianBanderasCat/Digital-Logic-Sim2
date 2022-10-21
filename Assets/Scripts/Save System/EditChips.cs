using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
using SimpleFileBrowser;


public class EditChips : MonoBehaviour
{
    [Header("References")]
    public Transform implementationHolder;
    public GameObject manager;
    public GameObject create;
    public ChipSignal InputSignalPrefab;
    public ChipSignal OutputSignalPrefab;
    public Wire wirePrefab;
    GameObject InputBar;
    GameObject OutputBar;


    Vector2 chipPos;
    private IEnumerator coroutine;

    public void OpenFileBrowser()
    {
        FileBrowser.SetFilters(false, ".txt");
        FileBrowser.ShowLoadDialog((path) => { DisplayAll(path); }, null, FileBrowser.PickMode.Files, false, SaveSystem.CurrentSaveProfileDirectoryPath, null, "Load Chip Save File", "Load");

    }


    void DisplayAll(string[] paths)
    {
        create.GetComponent<CreateMenu>().FinishCreation();
        string path = paths[0];
        DisplayChips(path);
    }

    public void DisplayChips(string chipPath)
    {

        ChipInteraction chipInteraction = GameObject.Find("Interaction").transform.Find("Chip Interaction").gameObject.GetComponent<ChipInteraction>();
        SavedChip savedChip;
        Chip loadingChip;
        List<Chip> loadedChips = new List<Chip>();

        using (StreamReader reader = new StreamReader(chipPath))
        {
            string chipSaveString = reader.ReadToEnd();
            savedChip = JsonUtility.FromJson<SavedChip>(chipSaveString);
        }

        string originalChipName = savedChip.name;
        foreach (SavedComponentChip componentChip in savedChip.savedComponentChips)
        {

            string chipName = componentChip.chipName;
            if ((chipName != "SIGNAL IN") && (chipName != "SIGNAL OUT"))
            {

                if (IsBuiltInChipName(chipName))
                {
                    for (int i = 0; i < manager.GetComponent<Manager>().builtinChips.Length; i++)
                    {
                        if (chipName == manager.GetComponent<Manager>().builtinChips[i].name)
                        {
                            chipPos = new Vector2((float)componentChip.posX, (float)componentChip.posY);
                            loadingChip = (GameObject.Instantiate(manager.GetComponent<Manager>().builtinChips[i], chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform).GetComponent<Chip>());
                            loadedChips.Add(loadingChip);
                            chipInteraction.allChips.Add(loadingChip);

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
                    loadingChip = (GameObject.Instantiate(original, chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform).GetComponent<Chip>());
                    loadedChips.Add(loadingChip);
                    chipInteraction.allChips.Add(loadingChip);

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
                    ChipSignal spawnedSignal = Instantiate(InputSignalPrefab, chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform.Find("Inputs"));
                    loadedChips.Add(spawnedSignal.GetComponent<Chip>());
                    InputBar.GetComponent<ChipInterfaceEditor>().signals.Add(spawnedSignal);
                    spawnedSignal.side = ChipSignal.Side.Left;

                }

                if (chipName == "SIGNAL OUT")
                {
                    chipPos = new Vector2((float)componentChip.posX, (float)componentChip.posY);
                    ChipSignal spawnedSignal = Instantiate(OutputSignalPrefab, chipPos, Quaternion.identity, GameObject.FindWithTag("ImplementationHolder").transform.Find("Outputs"));
                    loadedChips.Add(spawnedSignal.GetComponent<Chip>());
                    OutputBar.GetComponent<ChipInterfaceEditor>().signals.Add(spawnedSignal);
                    spawnedSignal.side = ChipSignal.Side.Right;

                }
            }
        }

        /*Dictionary<SavedWire, int[] > savedWires = new Dictionary<SavedWire, int[]>();                Will maybe do later (useful only for below)
        string wirePath = SaveSystem.GetPathToWireSaveFile(originalChipName);
        SavedWireLayout savedWireLayout;
        using (StreamReader reader = new StreamReader(wirePath))
        {
            string wireSaveString = reader.ReadToEnd();
            savedWireLayout = JsonUtility.FromJson<SavedWireLayout>(wireSaveString);
        }
        foreach (SavedWire savedWire in savedWireLayout.serializableWires)
        {
            savedWires.Add(savedWire, new int[] { savedWire.parentChipIndex, savedWire.parentChipOutputIndex });
        }*/

        //Code from ChipLoader.cs arranged to work here
        for (int chipIndex = 0; chipIndex < savedChip.savedComponentChips.Length; chipIndex++)
        {

            loadedChips.ToArray();
            Chip loadedComponentChip = loadedChips[chipIndex];
            for (int inputPinIndex = 0; inputPinIndex < loadedComponentChip.inputPins.Length; inputPinIndex++)
            {
                SavedInputPin savedPin = savedChip.savedComponentChips[chipIndex].inputPins[inputPinIndex];
                Pin pin = loadedComponentChip.inputPins[inputPinIndex];

                // If this pin should receive input from somewhere, then wire it up to that pin
                if (savedPin.parentChipIndex != -1)
                {

                    Pin connectedPin = loadedChips[savedPin.parentChipIndex].outputPins[savedPin.parentChipOutputIndex];
                    pin.cyclic = savedPin.isCylic;
                    Pin.TryConnect(connectedPin, pin);
                    if (Pin.TryConnect(connectedPin, pin))
                    {
                        Wire loadedWire = GameObject.Instantiate(wirePrefab, parent: GameObject.FindWithTag("ImplementationHolder").transform.Find("Wires"));
                        loadedWire.Connect(connectedPin, loadedComponentChip.inputPins[inputPinIndex]);

                        /*foreach (SavedWire savedWire in savedWires.Keys)                      Will maybe do later
                        {
                            if (savedWires[savedWire][0] == chipIndex && savedWires[savedWire][1] == inputPinIndex)
                            {
                                foreach (Vector2 anchorPoint in savedWire.anchorPoints)
                                {
                                    if (anchorPoint.x != -7.243164539337158 && anchorPoint.x != 7.243164539337158)
                                    {
                                        loadedWire.AddAnchorPoint(anchorPoint);
                                    }
                                }
                            }
                        }*/
                    }
                }
            }
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
        string[] builInNames = { "AND", "NOT", "CLOCK", "SCREEN", "7SEG DISP", "SYMB", "KEY", "RAND" };
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
using SimpleFileBrowser;

public class ChipImporter : MonoBehaviour
{

    //List of all used chips in a chip
    private List<SavedComponentChip> usedChips = new List<SavedComponentChip>();
    private List<String> usedChipsPaths = new List<String>();

    //All infos about the chip (at first the chip selected in the FileBrowser, then its components)
    private SavedChip chip;
    private string chipWireLayoutPath;
    private string chipSaveString;
    private string chipWireLayoutSaveString;
    private string chipName;


    //Destinations Paths
    private string destination;
    private string wireLayoutDestination;

    private int i;
    private bool tester;
    private Dictionary<String, List<string>> chipInfos = new Dictionary<string, List<string>>();


    public void OpenFileBrowser()
    {
        FileBrowser.SetFilters(false, ".txt");
        FileBrowser.ShowLoadDialog((path) => { Import(path); }, null, FileBrowser.PickMode.Files, false, SaveSystem.GlobalDirectoryPath, null, "Load Chip Save File", "Load") ;

    }
   
    // Import the save files
    void Import(string[] paths)
    {
        chipInfos.Clear();
        string path = paths[paths.Count() - 1];
        var manager = new Manager();
        tester = true;
        GetChipInfoByPath(path);
        List<SavedChip> savedChips = GetListOfSavedChips(chipInfos);
        savedChips = SortChipsByOrderOfCreation(savedChips);
        List<string> savedChipsPaths = new List<string>();
        foreach(SavedChip thisChip in savedChips)
        {
            savedChipsPaths.Add(Path.Combine(SaveSystem.GlobalDirectoryPath, thisChip.name + ".txt"));
            print(savedChipsPaths);
        }

        foreach (string thisPath in savedChipsPaths)
        {
            try
            {
                File.WriteAllText((chipInfos[thisPath])[1], (chipInfos[thisPath])[2]);
                File.WriteAllText((chipInfos[thisPath])[0], (chipInfos[thisPath])[3]);
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        manager.RefreshAll();

    }

    static List<SavedChip> SortChipsByOrderOfCreation(List<SavedChip> chips)
    {
        chips.Sort((a, b) => a.creationIndex.CompareTo(b.creationIndex));
        return chips;
    }

    //Get the SavedChip of a chip by its path
    public SavedChip GetSavedChipByPath(string thisChipPath)
    {
        string saveString = "";
        using (StreamReader reader = new StreamReader(thisChipPath))
        { saveString = reader.ReadToEnd(); }
        return JsonUtility.FromJson<SavedChip>(saveString);
    }

    private List<SavedChip> GetListOfSavedChips(Dictionary<String, List<string>> dict)
    {
        List<SavedChip> chipsList = new List<SavedChip>();

        foreach(string thisPath in dict.Keys)
        {
            Debug.Log(thisPath);
            chipsList.Add(GetSavedChipByPath(thisPath));
        }

        return chipsList;
    }

    //Get the useful info from the chip's save file, and return a Dictionnary which has all infos stored
    private void GetChipInfoByPath(string chipPath)
    {
        using (StreamReader reader = new StreamReader(chipPath))
        { chipSaveString = reader.ReadToEnd(); }

        chip = JsonUtility.FromJson<SavedChip>(chipSaveString);
        usedChips = chip.savedComponentChips.ToList();

        chipName = chip.name;

        if(tester)
        {
            chipPath  = Path.Combine(SaveSystem.GlobalDirectoryPath, chipName + ".txt");
            tester = false;
        }

        chipWireLayoutPath = Path.Combine(SaveSystem.GlobalWireLayoutDirectoryPath, chipName + ".txt");

        using (StreamReader reader = new StreamReader(chipWireLayoutPath))
        { chipWireLayoutSaveString = reader.ReadToEnd(); }

        destination = Path.Combine(SaveSystem.CurrentSaveProfileDirectoryPath, chipName + ".txt");
        wireLayoutDestination = Path.Combine(SaveSystem.CurrentSaveProfileWireLayoutDirectoryPath, chipName + ".txt");

        if(!chipInfos.ContainsKey(chipPath))
        {
            chipInfos.Add(chipPath, new List<string>() { wireLayoutDestination, destination, chipSaveString, chipWireLayoutSaveString });
        }            
    
    
        if (usedChips != null)
        {
            if (usedChips.Count >= 1)
            {
                GetUsedChipsInfos(usedChips);
            }
        }
    }
    
    //Get the useful info from components chip
    private void GetUsedChipsInfos(List<SavedComponentChip> usedChipsList)
    {
        usedChipsPaths.Clear();
        foreach (SavedComponentChip thisUsedChip in usedChipsList)
        {
            chipName = thisUsedChip.chipName;
            usedChipsPaths.Add(SaveSystem.GetPathToGlobalSaveFile(chipName));
            if (IsValidChipName(chipName))
            {
                GetChipInfoByPath(usedChipsPaths[usedChipsPaths.Count - 1]); 
            }
        }
    }


}
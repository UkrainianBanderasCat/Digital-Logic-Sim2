using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;
using SimpleFileBrowser;

public class ChipImporter : MonoBehaviour
{
    SavedChip importedChip;
    private string chipName;
    private string path;
    private string chipSaveString;
    private string WireLayoutSaveString;
    private string thisChipName;
    private string thisChipWireLayout;
    private string destination;
    private string wireDestination;

    private SavedComponentChip[] usedChips;
    private SavedComponentChip thisChip;

    public void OpenFileBrowser()
    {
        FileBrowser.SetFilters(false, ".txt");
        FileBrowser.ShowLoadDialog((path) => { ImportChip(path); }, null, FileBrowser.PickMode.Files, false, SaveSystem.GlobalDirectoryPath, null, "Load Chip Save File", "Load") ;

    }
    public void ImportChip(string[] paths)
    {
        path = paths[0];

        int i;
        using (StreamReader reader = new StreamReader(path))
        { chipSaveString = reader.ReadToEnd(); }

        importedChip = JsonUtility.FromJson<SavedChip>(chipSaveString);
        chipName = importedChip.name;
        destination = SaveSystem.CurrentSaveProfileDirectoryPath + "/" + chipName + ".txt";
        usedChips = importedChip.savedComponentChips;
        wireDestination = SaveSystem.CurrentSaveProfileWireLayoutDirectoryPath + "/" + chipName + ".txt";

        string wirePath = Path.Combine(SaveSystem.GlobalWireLayoutDirectoryPath, chipName + ".txt");

        try
        {
            File.WriteAllText(destination, chipSaveString);
        }
        catch (Exception)
        {

            throw;
        }

        try
        {
            using (StreamReader reader = new StreamReader(wirePath))
            { WireLayoutSaveString = reader.ReadToEnd(); }

            File.WriteAllText(wireDestination, WireLayoutSaveString);
        }
        catch (Exception)
        {

            throw;
        }
        


        for (i = 0; i < usedChips.Length; i++)
        {
            thisChip = usedChips[i];
            thisChipName = thisChip.chipName;
            destination = SaveSystem.CurrentSaveProfileDirectoryPath + "/" + thisChipName + ".txt";
            wireDestination = SaveSystem.CurrentSaveProfileWireLayoutDirectoryPath + "/" + thisChipName + ".txt";
            thisChipWireLayout = Path.Combine(SaveSystem.GlobalWireLayoutDirectoryPath, thisChipName + ".txt");
            path = Path.Combine(SaveSystem.GlobalDirectoryPath, thisChipName + ".txt");
            if (thisChipName != "SIGNAL IN" && thisChipName != "SIGNAL OUT" && thisChipName != "NOT" && thisChipName != "AND" && !ChipLoader.GetLoadedChips.ContainsKey(thisChipName))
            {

                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    { chipSaveString = reader.ReadToEnd(); }
                    File.WriteAllText(destination, chipSaveString);

                }
                catch (Exception)
                {

                    throw;
                }
                
                
                try
                {
                    using (StreamReader reader = new StreamReader(thisChipWireLayout))
                    { WireLayoutSaveString = reader.ReadToEnd(); }

                    File.WriteAllText(wireDestination, WireLayoutSaveString);
                }
                catch (Exception)
                {

                    throw;
                }
                
                
            }

            
        }

     
        
    }

}
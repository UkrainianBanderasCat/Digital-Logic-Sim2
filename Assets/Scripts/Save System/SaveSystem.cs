using System.IO;
using UnityEngine;

public static class SaveSystem {

	public static string activeProjectName = "Untitled";
	const string fileExtension = ".txt";

	public static void SetActiveProject (string projectName) {
		activeProjectName = projectName;
	}

	public static void Init () {
		// Create save directory (if doesn't exist already)
		Directory.CreateDirectory (CurrentSaveProfileDirectoryPath);
		Directory.CreateDirectory (CurrentSaveProfileWireLayoutDirectoryPath);
		Directory.CreateDirectory(GlobalDirectoryPath);
		Directory.CreateDirectory(GlobalWireLayoutDirectoryPath);
	}

	public static void LoadAll (Manager manager) {
		// Load any saved chips
		var sw = System.Diagnostics.Stopwatch.StartNew ();
		string[] chipSavePaths = Directory.GetFiles (CurrentSaveProfileDirectoryPath, "*" + fileExtension);
		
		ChipLoader.LoadAllChips (chipSavePaths, manager);

		GameObject.Find("Manager").GetComponent<EditChips>().DisplayChips(GetPathToSaveFile(""));
		Debug.Log ("Load time: " + sw.ElapsedMilliseconds);
		Debug.Log ("This directory path : " + CurrentSaveProfileDirectoryPath + ".The global directory path : " + GlobalDirectoryPath);



	}



	public static void LoadGlobal(Manager manager)
    {
		string[] globalChipSavePaths = Directory.GetFiles(GlobalDirectoryPath, "*" + fileExtension);
		ChipLoader.LoadAllChips(globalChipSavePaths, manager);
	}

	public static string GetNameOfProject()
	{
		return activeProjectName;
	}

	public static string GetPathToWorkspaceSaveFile()
	{
		return Path.Combine(CurrentSaveProfileDirectoryPath, fileExtension);

	}
	 
	public static string GetPathToSaveFile (string saveFileName) {
		return Path.Combine (CurrentSaveProfileDirectoryPath, saveFileName + fileExtension);
	}

	public static string GetPathToWireSaveFile (string saveFileName) {
		return Path.Combine (CurrentSaveProfileWireLayoutDirectoryPath, saveFileName + fileExtension);
	}

	public static string GetPathToGlobalSaveFile(string saveFileName)
	{
		return Path.Combine(GlobalDirectoryPath, saveFileName + fileExtension);
	}

	public static string GetPathToGlobalWireSaveFile(string saveFileName)
	{
		return Path.Combine(GlobalWireLayoutDirectoryPath, saveFileName + fileExtension);
	}

	public static string CurrentSaveProfileDirectoryPath {
		get {
			return Path.Combine (SaveDataDirectoryPath, activeProjectName);
		}
	}

	public static string CurrentSaveProfileWireLayoutDirectoryPath {
		get {
			return Path.Combine (CurrentSaveProfileDirectoryPath, "WireLayout");
		}
	}

	public static string GlobalDirectoryPath
	{
		get
		{
			return Path.Combine(SaveDataDirectoryPath, "Global");
		}
	}

	public static string GlobalWireLayoutDirectoryPath
	{
		get
		{
			return Path.Combine(GlobalDirectoryPath, "WireLayout");
		}
	}



	public static string[] GetSaveNames () {
		string[] savedProjectPaths = new string[0];
		if (Directory.Exists (SaveDataDirectoryPath)) {
			savedProjectPaths = Directory.GetDirectories (SaveDataDirectoryPath);
		}
		for (int i = 0; i < savedProjectPaths.Length; i++) {
			string[] pathSections = savedProjectPaths[i].Split (Path.DirectorySeparatorChar);
			savedProjectPaths[i] = pathSections[pathSections.Length - 1];
		}
		return savedProjectPaths;
	}

	public static string SaveDataDirectoryPath {
		get {
			const string saveFolderName = "SaveData";
			return Path.Combine (Application.persistentDataPath, saveFolderName);
		}
	}

	public static void DeleteProject(string nameProject)
	{
		Directory.Delete(SaveDataDirectoryPath + "/" + nameProject, true);
	}
}
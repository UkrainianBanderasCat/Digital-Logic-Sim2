using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using System;

public class TestScript : MonoBehaviour
{

    [Serializable]
    public class Translation
    {
        public string id;
        public string text;
    }

    public List<Translation> translations = new List<Translation>();

    public string selectedLanguage = "English";
    public List<String> avaibleLanguages = new List<String>();
    string directoryPath = "";
    public FileInfo[] info;
    
    void Awake()
    {
        directoryPath = Application.dataPath + "/StreamingAssets/Localization/";
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        info = dir.GetFiles("*.txt");

        Debug.Log(info);
        Debug.Log(info.Length);
        
        foreach (FileInfo f in info) 
        {   
            string filePath = f.ToString();
            Debug.Log(filePath);

            string fileNameWithTxt = filePath.Replace(directoryPath, "");
            avaibleLanguages.Add(fileNameWithTxt.Substring(0, fileNameWithTxt.Length - 4));

            StreamReader reader = new StreamReader(filePath); 
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        } 

        //int lines = File.ReadAllLines(directoryPath + selectedLanguage + ".txt").Length;
        string[] lines = System.IO.File.ReadAllLines(directoryPath + selectedLanguage + ".txt");
        for (int i = 0; i < lines.Length; i++)
        {
            Translation translation = new Translation();
            
            Debug.Log(lines[i]);
            string[] splitArray = lines[i].Split(char.Parse("="));

            translation.id = splitArray[0];
            translation.text = splitArray[1];

            translations.Add(translation); 
        }         
    }

    public string GetText(string id)
    {
        for (int i = 0; i < translations.Count; i++)
        {
            if (translations[i].id == id)
            {
                return translations[i].text;
            }
        }

        return "";
    }
}

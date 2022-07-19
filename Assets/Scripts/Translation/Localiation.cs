using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;
using System.IO;

public class Localiation : MonoBehaviour
{
    [System.Serializable]
    public class Translation
    {
        public string id;
        public string text;
    }

    public List<Translation> translations = new List<Translation>();
    
    public string selectedLanguage = "English";
    public List<string> avaibleLanguages = new List<string>();
    string directoryPath = "";
    public FileInfo[] info;
    public GameObject langButton;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        if (PlayerPrefs.GetString("selectedLanguage") != "")
            selectedLanguage = PlayerPrefs.GetString("selectedLanguage");

        directoryPath = Application.dataPath + "/StreamingAssets/Localization/";
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        info = dir.GetFiles("*.txt");

        //Debug.Log(info);
        //Debug.Log(info.Length);
        
        foreach (FileInfo f in info) 
        {   
            string filePath = f.ToString();
            //Debug.Log(filePath);

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
            
            //Debug.Log(lines[i]);
            string[] normalText = lines[i].Split(char.Parse("="));
            string[] colorfulText = Regex.Split(lines[i], @"\=\-");

            translation.id = normalText[0];
            if (normalText.Length == 2)
                translation.text = normalText[1];
            
            else if (normalText.Length > 2)
                translation.text = colorfulText[1];

            translations.Add(translation); 
        }    

        langButton.transform.GetComponentInChildren<TMPro.TMP_Text> ().text = selectedLanguage;
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

    public void ChangeLanguage()
    {
        int nextLanguage = avaibleLanguages.IndexOf(selectedLanguage);
        nextLanguage++;
        if (nextLanguage > avaibleLanguages.Count - 1)
            nextLanguage = 0;

        Debug.Log(avaibleLanguages.Count);
        selectedLanguage = avaibleLanguages[nextLanguage];
        PlayerPrefs.SetString("selectedLanguage", selectedLanguage);
        langButton.transform.GetComponentInChildren<TMPro.TMP_Text> ().text = selectedLanguage;
    }

}

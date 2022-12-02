using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logs : MonoBehaviour
{
    string filepath = "";
    //string filename = "";

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    void OnApplicationQuit()
    {
        filepath = Application.dataPath + "/StreamingAssets/Logs/Log.txt";

        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        filepath = Application.dataPath + "/StreamingAssets/Logs/Log.txt";
        //filename = "Log.txt";
        
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Logs"))
        {
            Directory.CreateDirectory(Application.dataPath + "/StreamingAssets/Logs");
        }
        
        if (!File.Exists(filepath))
        {
            FileStream fs = File.Create(filepath);
            fs.Close();
        }

        if( new FileInfo(filepath).Length == 0)
        {
            TextWriter tw = new StreamWriter(filepath, true);

            tw.Write("Operative System: " + SystemInfo.operatingSystem + "\n" +
                        "RAM size: " + SystemInfo.systemMemorySize + "\n" + 
                        "Processor: " + SystemInfo.processorType + "\n" +
                        "Graphic card: " + SystemInfo.graphicsDeviceName + "\n" +
                        "-----------------------------------------");

            tw.Close();
        }
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filepath, true);

        tw.WriteLine("[" + System.DateTime.Now + "] " + logString);

        tw.Close();
    }
}

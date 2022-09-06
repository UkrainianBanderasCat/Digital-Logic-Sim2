using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Start()
    {
        instance = this;
    }

    public void LoadScene(string name)
    {
        LoadingScreenDataTransfer.sceneName = name;
        SceneManager.LoadScene("Loading Screen");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    void Start()
    {
        SceneManager.LoadSceneAsync(LoadingScreenDataTransfer.sceneName);
    }

    void Update()
    {
        
    }


}

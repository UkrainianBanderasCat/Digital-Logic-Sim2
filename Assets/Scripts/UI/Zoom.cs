using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Vector3 scaleChange;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown("=")) {
            //ZoomIn
        }

        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown("-")) {
            //ZoomOut
        } 
    }
}

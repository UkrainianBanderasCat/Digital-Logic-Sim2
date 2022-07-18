using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestScript : MonoBehaviour
{
    public List<string> chars = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString!="")
        {
            chars = new List<string>();
            char tmp=Input.inputString[0];
            int temp=(int)tmp;

            Debug.Log(temp.ToString());
        }
    }
}

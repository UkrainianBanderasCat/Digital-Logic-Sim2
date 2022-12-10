using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChipSevenSegementDisp : MonoBehaviour
{
    public SegmentDisplay sevenSegmentDisp;
    
    public GameObject[] leds;
    
    public Material on;
    public Material off;
    
    bool work = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (work)
        {
            for (int i = 0; i < leds.Length; i++) 
            {
                if (sevenSegmentDisp.inputPins[i].currentState == 1)
                {
                    leds[i].GetComponent<MeshRenderer>().material = on;
                }

                if (sevenSegmentDisp.inputPins[i].currentState == 0)
                {
                    leds[i].GetComponent<MeshRenderer>().material = off;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockSpeedController : MonoBehaviour
{
    void Update()
    {
        float v = gameObject.GetComponent<Slider>().value;
        GameObject.Find("Simulation").GetComponent<Simulation>().SetClockSpeed(v);
    }
}

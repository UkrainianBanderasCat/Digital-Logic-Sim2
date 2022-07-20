using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSpeedController : MonoBehaviour
{
    void Update()
    {
        float v = gameObject.GetComponent<UnityEngine.UI.Slider>().value;
        GameObject.Find("Simulation").GetComponent<Simulation>().SetClockSpeed(v);
    }
}

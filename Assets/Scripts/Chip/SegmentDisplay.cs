using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentDisplay : BuiltinChip
{

    public Material on;
    public Material off;
    public GameObject[] leds;
    protected override void Awake () {
		base.Awake ();
	}

	protected override void ProcessOutput () {
        for (int i = 0; i<leds.Length; i++) {
            if (inputPins[i].State == 1) {
                leds[i].GetComponent<MeshRenderer>().material = on;
            }

            else if (inputPins[i].State == 0) {
                leds[i].GetComponent<MeshRenderer>().material = off;
            }
        }
	}
}

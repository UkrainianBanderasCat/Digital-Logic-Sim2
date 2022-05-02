using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : BuiltinChip
{
    public GameObject[] leds;

    public int width;
    public int height;

    public int[][] position;
    public int cell;

    protected override void Awake () {
		base.Awake ();
	}

	protected override void ProcessOutput () {
        
	}
}

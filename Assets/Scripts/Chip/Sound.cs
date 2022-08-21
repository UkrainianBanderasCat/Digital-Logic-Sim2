using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Sound : BuiltinChip {

	[Range(0,2000)]
    public float frequency1;
 
	[Range(0,2000)]
    public float frequency2;
 
    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;
 
    AudioSource audioSource;
    int timeIndex = 0;
	int prevInput;

	void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically
    }

	protected override void ProcessOutput (int[] input) {
		if (input[0] != prevInput)
		{
			timeIndex = 0;
            audioSource.Play();
		}

		prevInput = input[0];
	}

	void OnAudioFilterRead(float[] data, int channels)
    {
        for(int i = 0; i < data.Length; i+= channels)
        {          
            data[i] = CreateSine(timeIndex, frequency1, sampleRate);
           
            if(channels == 2)
                data[i+1] = CreateSine(timeIndex, frequency2, sampleRate);
           
            timeIndex++;
           
            //if timeIndex gets too big, reset it to 0
            if(timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }
   
    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sign(Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate));
    }

}
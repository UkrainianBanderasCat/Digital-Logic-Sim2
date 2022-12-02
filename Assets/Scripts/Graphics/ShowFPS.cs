using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowFPS : MonoBehaviour
{
    int m_frameCounter = 0;

    float m_timeCounter = 0.0f;

    float m_lastFramerate = 0.0f;

    public float m_refreshTime = 0.5f;

    public TMPro.TMP_Text fpsDisplay;

    public bool showFPS;

    // Start is called before the first frame update
    // void Awake()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }
    void Start()
    {
        showFPS = PlayerPrefs.GetInt("showFPS") == 0 ? false : true;
    }

    void Update()
    {
        if (Input.GetKeyDown("b"))
            showFPS = !showFPS;

        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float) m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }

        //Debug.Log(m_lastFramerate);
        fpsDisplay.text = "FPS " + Convert.ToInt32(m_lastFramerate - 0.5f);
        fpsDisplay.enabled = showFPS;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelShaderController : MonoBehaviour
{
    public Material mat;

    void Start()
    {
        //mat = GetComponent<UnityEngine.UI.Image>().material;
        mat.SetFloat("_Fill", -5f);
    }

    public void FillOn()
    {
        mat.SetFloat("_Fill", -5f);
        mat.SetInt("_On", 1);
        StartCoroutine(Fill());
    }

    public void FillOff()
    {
        mat.SetFloat("_Fill", -5f);
        mat.SetInt("_On", 0);
        StartCoroutine(Fill());
    }

    IEnumerator Fill()
    {
        mat.SetFloat("_Fill", mat.GetFloat("_Fill") + mat.GetFloat("_Speed") / 100);
        yield return new WaitForSeconds(0.01f);
        if (mat.GetFloat("_Fill") < 5f)
        {
            StartCoroutine(Fill());
        }
    }
}
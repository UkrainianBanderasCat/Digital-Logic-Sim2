using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    public string id;

    void Update()
    {
        #if UNITY_STANDALONE
        Localiation transl = GameObject.Find("Translation").GetComponent<Localiation>();
        TMP_Text text = GetComponent<TMP_Text>();
        //text.font = 

        text.text = transl.GetText(id);
        #endif
    }
}

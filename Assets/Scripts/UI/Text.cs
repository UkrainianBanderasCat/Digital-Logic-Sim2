using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    public string id;

    void Update()
    {
        /* #if UNITY_STANDALONE                             DOESN'T WORK
        Localiation transl = GameObject.Find("Translation").GetComponent<Localiation>();
        TMP_Text text = GetComponent<TMP_Text>();
        text.font =  -- Originally a Comment 

        text.text = transl.GetText(id);
        #endif */
    }
}

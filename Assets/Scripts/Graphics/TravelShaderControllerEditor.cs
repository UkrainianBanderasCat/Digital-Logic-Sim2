using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TravelShaderController))]
public class TravelShaderControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TravelShaderController myScript = (TravelShaderController)target;
        if(GUILayout.Button("Fill On"))
        {
            myScript.FillOn();
        }

        if(GUILayout.Button("Fill Off"))
        {
            myScript.FillOff();
        }
    }
}

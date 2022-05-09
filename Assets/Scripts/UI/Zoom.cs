using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Vector3 scaleChange;
    Vector3 scale = new Vector3(1, 1, 1);
    public float min;
    public float max;
    float interactionNum = 0.25f;
    public GameObject[] objectsToZoom;
    public ChipInteraction _chipInteraction;

    void Update()
    {
        _chipInteraction = GameObject.FindWithTag("Interaction").GetComponent<ChipInteraction>(); 
        objectsToZoom = GameObject.FindGameObjectsWithTag("Zoom");
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown("=") && scale.x < max) {
            scale += scaleChange;
            interactionNum += 0.1f;
        }

        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown("-") && scale.x > min) {
            scale -= scaleChange;
            interactionNum -= 0.1f;
        }

        _chipInteraction.selectionBoundsBorderPadding = interactionNum;
        for (int i = 0; i < objectsToZoom.Length; i++) {
            objectsToZoom[i].transform.localScale = scale;
        }
    }
}

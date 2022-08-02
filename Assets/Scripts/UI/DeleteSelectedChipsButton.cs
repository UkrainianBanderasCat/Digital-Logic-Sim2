using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelectedChipsButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Delete);
    }

    // Update is called once per frame
    void Delete()
    {
        GameObject.Find("Chip Interaction").GetComponent<ChipInteraction>().DeleteSelectedChips();
    }
}

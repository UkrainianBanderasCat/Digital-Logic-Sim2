using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorkspace : MonoBehaviour
{
    public GameObject implementionHolder;
    public GameObject inputs;
    public GameObject outputs;

    private Vector3 dragOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        implementionHolder = GameObject.Find("Implementation Holder");
        
        inputs = implementionHolder.transform.GetChild(0).gameObject;
        outputs = implementionHolder.transform.GetChild(1).gameObject;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            EdgeCollider2D[] wires = implementionHolder.transform.GetChild(3).GetComponentsInChildren<EdgeCollider2D>();
            foreach (EdgeCollider2D wire in wires)
                wire.offset += new Vector2(0, 0.1f);

            implementionHolder.transform.position -= new Vector3(0, 0.1f, 0);    
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            EdgeCollider2D[] wires = implementionHolder.transform.GetChild(3).GetComponentsInChildren<EdgeCollider2D>();
            foreach (EdgeCollider2D wire in wires)
                wire.offset -= new Vector2(0, 0.1f);
            
            implementionHolder.transform.position += new Vector3(0, 0.1f, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            implementionHolder.transform.position -= new Vector3(0.1f, 0, 0);
        
            inputs.transform.position += new Vector3(0.1f, 0, 0);
            outputs.transform.position -= new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            implementionHolder.transform.position += new Vector3(0.1f, 0, 0);
        
            inputs.transform.position -= new Vector3(0.1f, 0, 0);
            outputs.transform.position += new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.R))
        {
            EdgeCollider2D[] wires = implementionHolder.transform.GetChild(3).GetComponentsInChildren<EdgeCollider2D>();
            foreach (EdgeCollider2D wire in wires)
                wire.offset = new Vector2(0, 0);

            implementionHolder.transform.position = new Vector3(0, 0, 0);
            inputs.transform.position = new Vector3(0, 0, 0);
            outputs.transform.position = new Vector3(0, 0, 0);
        }
    }

    // void PanMovement() DOESNT WORK
    // {
    //     if (Input.GetMouseButtonDown(2))
    //         dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     if (Input.GetMouseButton(2))
    //     {
    //         Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    //         implementionHolder.transform.position += difference / 2;
    //     }
    // }
}

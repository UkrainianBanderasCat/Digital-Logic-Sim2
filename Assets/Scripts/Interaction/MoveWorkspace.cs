using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorkspace : MonoBehaviour
{
    public GameObject implementionHolder;
    public GameObject inputs;
    public GameObject outputs;
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

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.W))
        {
            EdgeCollider2D[] wires = implementionHolder.transform.GetChild(3).GetComponentsInChildren<EdgeCollider2D>();
            foreach (EdgeCollider2D wire in wires)
                wire.offset -= new Vector2(0, 0.1f);

            implementionHolder.transform.position += new Vector3(0, 0.1f, 0);    
        }

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.S))
        {
            EdgeCollider2D[] wires = implementionHolder.transform.GetChild(3).GetComponentsInChildren<EdgeCollider2D>();
            foreach (EdgeCollider2D wire in wires)
                wire.offset += new Vector2(0, 0.1f);
            
            implementionHolder.transform.position -= new Vector3(0, 0.1f, 0);
        }

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.D))
        {
            implementionHolder.transform.position += new Vector3(0.1f, 0, 0);
        
            inputs.transform.position -= new Vector3(0.1f, 0, 0);
            outputs.transform.position += new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.A))
        {
            implementionHolder.transform.position -= new Vector3(0.1f, 0, 0);
        
            inputs.transform.position += new Vector3(0.1f, 0, 0);
            outputs.transform.position -= new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.R))
        {
            EdgeCollider2D[] wires = implementionHolder.transform.GetChild(3).GetComponentsInChildren<EdgeCollider2D>();
            foreach (EdgeCollider2D wire in wires)
                wire.offset = new Vector2(0, 0);

            implementionHolder.transform.position = new Vector3(0, 0, 0);
            inputs.transform.position = new Vector3(0, 0, 0);
            outputs.transform.position = new Vector3(0, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorkspace : MonoBehaviour
{
    public GameObject implementionHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        implementionHolder = GameObject.Find("Implementation Holder");

        if (Input.GetKey("w"))
        {
            implementionHolder.transform.position += new Vector3(0, 0.1f, 0);
        }

        if (Input.GetKey("s"))
        {
            implementionHolder.transform.position -= new Vector3(0, 0.1f, 0);
        }

        if (Input.GetKey("d"))
        {
            implementionHolder.transform.position += new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            implementionHolder.transform.position -= new Vector3(0.1f, 0, 0);
        }
    }
}

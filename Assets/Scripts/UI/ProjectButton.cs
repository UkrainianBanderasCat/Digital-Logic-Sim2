using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectButton : MonoBehaviour
{
    public string name;
    bool onHover;

    void Update()
    {
        if (onHover)
            Delete();
    }

    public void Delete()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            SaveSystem.DeleteProject(name); 
        }
    }

    public void Hover(bool _onHover)
    {
        onHover = _onHover;
    }
}

using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
 
public class InvertedToggleEvent : MonoBehaviour {

    public UnityEngine.Events.UnityEvent<bool> onValueChangedInverse;
 
 
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener( (on)=>{ onValueChangedInverse.Invoke(!on); } );
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
 
public class MoreToggleEvents : MonoBehaviour {

    public UnityEvent<bool> onValueChangedInverse;
    public UnityEvent onValueTurnedOn;
    public UnityEvent onValueTurnedOff;

 
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((on) => {
            onValueChangedInverse?.Invoke(!on);
            if (on) {
                onValueTurnedOn?.Invoke();
            } else {
                onValueTurnedOff?.Invoke();
            }
        });
    }
}
using UnityEngine;

// Provides input signal (0 or 1) to a chip.
// When designing a chip, this input signal can be manually set to 0 or 1 by the player.
public class InputSignal : ChipSignal {

	public bool clicked;

	protected override void Start () {
		base.Start ();
		SetCol ();
	}

	public void Update()
	{
        if (Input.touchCount == 1) {
        	Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        	RaycastHit hit;
        	Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
        	if(Physics.Raycast(ray, out hit))
         	{
            	Debug.Log(hit.transform.name);
             	if (hit.collider != null) 
				{
            		GameObject touchedObject = hit.transform.gameObject;
                 	if (touchedObject.transform.name == gameObject.transform.name)
					{
						ToggleActive();
					}
             	}
         	}
  		}
 	}

	public void ToggleActive () {
		currentState = 1 - currentState;
		SetCol ();
	}

	public void SendSignal (int signal) {
		currentState = signal;
		outputPins[0].ReceiveSignal (signal);
		SetCol ();
	}

	public void SendSignal () {
		gameObject.tag = "Zoom";
		outputPins[0].ReceiveSignal (currentState);
	}

	void SetCol () {
		SetDisplayState (currentState);
	}

	public override void UpdateSignalName (string newName) {
		base.UpdateSignalName (newName);
		outputPins[0].pinName = newName;
	}

	void OnMouseDown () {
		//Debug.Log("Stop");
		ToggleActive ();
	}
}
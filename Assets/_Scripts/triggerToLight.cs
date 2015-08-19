using UnityEngine;
using System.Collections;

public class triggerToLight : MonoBehaviour {

	public GameObject listener;
	private bool triggered = false;

	private AICamera cam;

	void Start ()
	{
		cam = (AICamera) listener.GetComponent(typeof(AICamera));
	}


	void OnTriggerStay2D(Collider2D other){
		Debug.Log ("trigger sent");
		if(Input.GetKeyDown(KeyCode.F)){
			if (!triggered) {
				triggered = true;
				cam.isActive(false);
			} else {
				triggered = false;
				cam.isActive(true);
			}
		}
	}
}

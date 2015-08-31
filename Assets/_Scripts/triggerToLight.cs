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
		if(Input.GetKeyDown(KeyCode.F) && other.tag == "Player" && cam != null){

				cam.isActive(false);

		}
	}
}

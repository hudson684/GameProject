using UnityEngine;
using System.Collections;

public class triggerToLight : MonoBehaviour {

	public GameObject listener;
	private bool inRange = false;

	private AICamera cam;

	void Start ()
	{
		cam = (AICamera) listener.GetComponent(typeof(AICamera));
	}

	void Update(){
		if(inRange){
			if(Input.GetKeyDown(KeyCode.F)){
				cam.isActive(false);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			inRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			inRange = false;
		}

	}
	/*
	void OnTriggerStay2D(Collider2D other){
		if(Input.GetKeyDown(KeyCode.F) && other.tag == "Player" && cam != null){

				cam.isActive(false);

		}
	}
	*/
}

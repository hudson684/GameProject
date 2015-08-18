using UnityEngine;
using System.Collections;

public class TriggerDisplayText : MonoBehaviour {

	private Canvas Display;
	private bool inRange = false;
	
	// Use this for initialization
	void Start () {
		Display = GetComponentInChildren<Canvas>();
		Display.enabled =false;
	}

	void Update(){
		if(inRange){
			if(Input.GetKey(KeyCode.F)){
				toggleCanvas();
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
			Display.enabled = false;
		}
	}

	void toggleCanvas(){
		Display.enabled = !Display.enabled;
	}
}

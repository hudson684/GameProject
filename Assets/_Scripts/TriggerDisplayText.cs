using UnityEngine;
using System.Collections;

public class TriggerDisplayText : MonoBehaviour {

	public Canvas Display;
	private bool inRange = false;
	
	// Use this for initialization
	void Start () {
		Display.enabled =false;
	}

	void Update(){
		if(inRange){
			if(Input.GetKeyDown(KeyCode.F)){
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

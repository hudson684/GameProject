using UnityEngine;
using System.Collections;

public class displayText : MonoBehaviour {

	private Canvas Display;

	// Use this for initialization
	void Start () {
		Display = GetComponentInChildren<Canvas>();
		Display.enabled =false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && Display != null){
			Display.enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player" && Display != null){
			Display.enabled = false;
		}
	}
}

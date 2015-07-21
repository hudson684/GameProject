using UnityEngine;
using System.Collections;

public class playerStealth : MonoBehaviour {

	public GameObject playerObject;
	public bool isStealthed = false;



	void Update(){

		if (Input.GetKeyDown(KeyCode.G)) {
			if (isStealthed) {
				Debug.Log("untealth");
				isStealthed = false;
				playerObject.SetActive(true);
			}
			
		}

	}


	void OnTriggerStay2D(Collider2D other){

		if (Input.GetKeyDown(KeyCode.G)) {
			if (!isStealthed) {
				Debug.Log("Stealth");
				isStealthed = true;
				playerObject.SetActive(false);
			} 
			
		}
	}



}

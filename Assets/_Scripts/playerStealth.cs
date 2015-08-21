using UnityEngine;
using System.Collections;

public class playerStealth : MonoBehaviour {

	private GameObject playerObject;
	private bool isStealthed = false;
	public float waitTime = 0.5f;


	void Update(){

		if (Input.GetKeyDown(KeyCode.F)) {
			if (isStealthed) {
				StartCoroutine("wait");
			}
			
		}

	}


	void OnTriggerStay2D(Collider2D other){

		if (Input.GetKeyDown(KeyCode.F)) {
			if (!isStealthed) {
				if(other.tag == "Player"){
					Debug.Log("stealth");
					playerObject = other.gameObject;
					StartCoroutine("wait");
				}
			} 
			
		}
	}



	IEnumerator wait(){

		playerObject.SetActive(isStealthed);
		yield return new WaitForSeconds (waitTime);
		isStealthed = !isStealthed;

	}








}

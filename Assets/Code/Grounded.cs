using UnityEngine;
using System.Collections;

public class Grounded : MonoBehaviour {


	public bool isGrounded = false;

	private void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Ground" || other.tag == "Platform" || other.tag == "Object") {
			isGrounded = true;
		}
	}


	private void OnTriggerExit2D(Collider2D other){
		isGrounded = false;
	}


	public bool getGrounded(){
		return isGrounded;
	}


}

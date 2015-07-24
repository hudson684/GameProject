using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

	public Key[] Keys;
	public bool locked = true;

	void Start(){
		if(Keys.Length == 0){
			locked = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D other){
		if(other.transform.tag == "Player"){

			switch(locked){
			case true: // check if door ahs been unlocked
				locked = checkKeys();
				break;
			case false: 
				open();
				break;
			}
		}
	}//end OnCollisonEnter2D

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			close ();
		}
	}
	

	bool checkKeys(){
		for(int i = 0; i< Keys.Length; i++){
			if(Keys[i].getLocked()){
				return true;
			}
		}
		return false;
	}

	void open(){
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
	}

	void close(){
		gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
	}

	void OnDrawGizmos(){
		for(int i = 0; i < Keys.Length; i++){
			if(Keys[i] != null){
				Gizmos.color = Color.red;
				Gizmos.DrawLine(gameObject.transform.position, Keys[i].transform.position);
			}
		}
	}
}

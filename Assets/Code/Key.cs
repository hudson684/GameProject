using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	private bool isLocked = true;

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			//Player can access key
			if(Input.GetKey(KeyCode.F)){
				//Player Accesses Key
				Unlock();
			}
		}
	}

	void Unlock(){
		isLocked = false;
	}

	public bool getLocked(){
		return isLocked;
	}
}

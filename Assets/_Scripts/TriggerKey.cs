using UnityEngine;
using System.Collections;

public class TriggerKey : Key {

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			//Player can access key
			if(Input.GetKey(KeyCode.F)){
				//Player Accesses Key
				Unlock();
			}
		}
	}
}

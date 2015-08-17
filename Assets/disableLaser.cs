using UnityEngine;
using System.Collections;

public class disableLaser : MonoBehaviour {

	//public GameObject[] effectedTargets;
	public sweeperLaser[] effectedSweepers;

	void Start(){
	}
	
	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			//Player can access key
			if(Input.GetKey(KeyCode.F)){
				for(int i = 0; i< effectedSweepers.Length; i++){
					effectedSweepers[i].toggleOff();
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class OcclusionManager : MonoBehaviour {

	Occluder[] occluders;
	public float dampening;
	

	// Use this for initialization
	void Start () {
		occluders = transform.GetComponentsInChildren<Occluder>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			for(int i = 0; i < occluders.Length; i++){
				occluders[i].Toggle = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			for(int i = 0; i < occluders.Length; i++){
				occluders[i].Toggle = true;
			}
		}
	}
}

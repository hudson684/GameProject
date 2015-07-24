using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	private GameObject deathNodeObject;
	private DeathNode deathNode;


	void Start(){
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponentInParent(typeof(DeathNode));

	}


	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			deathNode.setDeath(true);
		} 
		if(other.tag == "Object"){
			other.GetComponent<BoxControlelr>().destroyBox();
		}
	}
}

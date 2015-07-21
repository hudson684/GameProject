using UnityEngine;
using System.Collections;

public class DeathByBullet : MonoBehaviour {
	private GameObject deathNodeObject;
	private DeathNode deathNode;
	
	
	void Start(){
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponentInParent(typeof(DeathNode));
		
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			deathNode.setDeath(true);
			Destroy(this.gameObject);
		} 
	}
}

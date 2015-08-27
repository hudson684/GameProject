using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	
	public GameObject CheckPointMarker;

	private GameObject deathNodeObject;
	private DeathNode deathNode;


	void Start(){
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponent(typeof(DeathNode));
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player") {
			Debug.Log("hit by player");
			Debug.Log(!deathNode.getDeath());
			if(!deathNode.getDeath()){
				Debug.Log("should move checkpoint marker");
				CheckPointMarker.transform.position = transform.position;
				Destroy(gameObject);

			}
		}
	}	
}

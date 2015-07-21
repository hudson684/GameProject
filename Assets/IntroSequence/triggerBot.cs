using UnityEngine;
using System.Collections;

public class triggerBot : MonoBehaviour {

	public GameObject Target;
	private Partol2 TargetComponent;
	private BoxCollider2D Trigger;

	// Use this for initialization
	void Start () {
		Trigger = gameObject.GetComponent<BoxCollider2D>();
		TargetComponent = Target.GetComponent<Partol2>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			activate();
		}
	}

	void activate(){
		TargetComponent.enabled = true;
	}
}
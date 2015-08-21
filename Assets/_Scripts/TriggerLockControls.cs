using UnityEngine;
using System.Collections;

public class TriggerLockControls : MonoBehaviour {


	public bool canGrapple;
	public bool canZeroGrav;


	//the objects that the player will need to interact with
	private GameObject deathNodeObject;
	private ControlNode contNode;

	// Use this for initialization
	void Start () {

		//initialize contact with the control node
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		contNode = (ControlNode) deathNodeObject.GetComponent(typeof(ControlNode));

	
	}


	void OnTriggerStay2D(Collider2D other){

		if (other.tag == "Player")
		{
			contNode.setZeroGrav(canZeroGrav);
			contNode.setCanGrapple(canGrapple);
		}
	}
}

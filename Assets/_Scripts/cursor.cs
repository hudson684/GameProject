using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {

	private static cursor mouse;

	private GameObject deathNodeObject;
	private ControlNode contNode;
	
	public Sprite normal;
	public Sprite toFar; 

	private bool isOn = true;

	private float disFromPlayer;

	// Use this for initialization
	void Awake () {
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		contNode = (ControlNode) deathNodeObject.GetComponent(typeof(ControlNode));

		if (mouse == null) {
			DontDestroyOnLoad (this.gameObject);
			mouse = this;
		} else if (mouse != this) {
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {

		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		contNode = (ControlNode) deathNodeObject.GetComponent(typeof(ControlNode));


		float mousex = Input.mousePosition.x;
		float mousey = Input.mousePosition.y;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mousex ,mousey ,10));
		transform.position = mousePosition;

		isOn = contNode.getCanGrapple ();
		this.GetComponent<SpriteRenderer>().enabled = isOn;

		disFromPlayer = Vector3.Distance (this.transform.position, contNode.getPlayerPosition ());

		if (isOn) {

			if (disFromPlayer > contNode.getGrappleDistance ()) {

				this.GetComponent<SpriteRenderer> ().sprite = toFar;
			} else {
				this.GetComponent<SpriteRenderer> ().sprite = normal;
			}
		} 

	}

	public float getDistance(){

		return disFromPlayer;
	}



}

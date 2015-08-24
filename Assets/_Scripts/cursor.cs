using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {

	private static cursor mouse;

	private GameObject deathNodeObject;
	private ControlNode contNode;
	
	public Sprite normal;
	public Sprite toFar; 


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
		float mousex = Input.mousePosition.x;
		float mousey = Input.mousePosition.y;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mousex ,mousey ,10));
		transform.position = mousePosition;



		if (Vector3.Distance (this.transform.position, contNode.getPlayerPosition ().position) > contNode.getGrappleDistance ()) {

			this.GetComponent<SpriteRenderer>().sprite = toFar;
			Debug.Log ("too far");
		} else {
			this.GetComponent<SpriteRenderer>().sprite = normal;
		}

	}



}

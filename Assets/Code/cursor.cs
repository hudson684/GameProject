using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float mousex = Input.mousePosition.x;
		float mousey = Input.mousePosition.y;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mousex ,mousey ,10));
		transform.position = mousePosition;

	}
}

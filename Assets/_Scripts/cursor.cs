using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {

	private static cursor mouse;

	// Use this for initialization
	void Awake () {
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

	}
}

using UnityEngine;
using System.Collections;

public class MainMenuCursor : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		float mousex = Input.mousePosition.x;
		float mousey = Input.mousePosition.y;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (mousex ,mousey ,10));

		Debug.Log (mousePosition.ToString ());

		transform.position = mousePosition;
	}
	
}

using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
			Debug.Log ("visable");
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Player")
			Debug.Log ("Not Vissable");
	}
}

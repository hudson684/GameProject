using UnityEngine;
using System.Collections;

public class DestroyBox : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Object"){
			other.GetComponent<BoxControlelr>().destroyBox();
		}
	}
}

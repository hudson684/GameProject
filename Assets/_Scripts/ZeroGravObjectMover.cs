using UnityEngine;
using System.Collections;

public class ZeroGravObjectMover : MonoBehaviour {
	/// <summary>
	/// The direction you want the zone to push an object towards
	///	positive y means up, positive x means right. eg
	/// 4, 0 means pushing the object up, -1, -1 means pushing the object toward the bottom left
	/// 
	///</summary>
	public Vector2 directionOfForce;

	void OnTriggerStay2D(Collider2D other){
		Debug.Log ("something has entered");

		if (other.gameObject.layer == LayerMask.NameToLayer ("Object")) {
			Debug.Log("should be adding force");
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(directionOfForce);
		}
	}


}

using UnityEngine;
using System.Collections;

public class Death_Lazer : MonoBehaviour {

	public 

	void  OnTriggerEnter2D(Collider2D col) {
		Debug.Log ("COLLIDED");
		if (col.gameObject.tag == "Player" ) {
			PlayerControl player = (PlayerControl) col.gameObject.GetComponent(typeof(PlayerControl));
			player.Kill();
		}
	}
}



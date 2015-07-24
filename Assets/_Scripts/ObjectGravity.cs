using UnityEngine;
using System.Collections;

public class ObjectGravity : MonoBehaviour {

	public float FULL_GRAVITY_SCALE = 3;
	public float HALF_GRAVITY_SCALE = 1.5f;
	public float ZERO_GRAVITY_SCALE = 0;

	void OnTriggerStay2D(Collider2D other){
		
		//All gravity zones in this game are defigned by a large invisable sprite that covers the entire 
		//area that uses the specific gravity in question.
		
		if(other.tag == "zeroGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale = ZERO_GRAVITY_SCALE;
			GetComponent<Rigidbody2D>().fixedAngle = false;
			
		} else if (other.tag == "halfGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale = HALF_GRAVITY_SCALE;
			//GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = false;
			
		} else if (other.tag == "fullGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale= FULL_GRAVITY_SCALE;
			//GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = false;
		}
	}
}

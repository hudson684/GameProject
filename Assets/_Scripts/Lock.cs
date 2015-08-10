using UnityEngine;
using System.Collections;

/// <summary>
/// Lock
/// 
/// Assesses the state of a door based on the states of associated keys.
/// While one or more keys are locked, the door remains locked
/// 
/// If no keys assigned to a lock, lock is automatically open.
/// </summary>
public class Lock : MonoBehaviour {

	//All keys associated to lock
	public Key[] Keys;
	public bool locked = true;

	//Setup for indicator Lights
	public GameObject indicator;
	public Vector3 padding;
	private Vector3 origin;

	void Start(){
		//Set origin for indicator lights
		origin = gameObject.transform.FindChild("IndicatorOrigin").transform.position;
		if(Keys.Length == 0){//if no keys, door is automatically unlocked
			locked = false;
			Instantiate(indicator, origin, Quaternion.identity);
		}else{//if keys greater than zero, instantiate an indicator for each key
			for(int i = 0; i < Keys.Length; i++){
				//create an indicator and set its key
				GameObject anIndicator = Instantiate(indicator, origin + (padding * i), Quaternion.identity) as GameObject;
				anIndicator.GetComponent<Indicator>().setKey(Keys[i]);
			}
		}
	}

	/// <summary>
	/// Raises the collision stay2 d event.
	/// 
	/// If the player collides with the door. Door assesses key for state
	/// </summary>
	/// <param name="other">Player</param>
	void OnCollisionStay2D(Collision2D other){
		if(other.transform.tag == "Player"){

			switch(locked){
			case true: // check if door ahs been unlocked
				locked = checkKeys();
				break;
			case false: 
				open();
				break;
			}
		}
	}//end OnCollisonEnter2D

	/// <summary>
	/// Raises the trigger exit2 d event.
	/// 
	/// Makes the doors collider solid
	/// </summary>
	/// <param name="other">Player</param>
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			close ();
		}
	}
	
	/// <summary>
	/// Checks if all keys are set to locked;
	/// </summary>
	/// <returns><c>true</c> one or more keys are still locked
	/// <c>false</c> all keys are ulocked</returns>
	bool checkKeys(){
		for(int i = 0; i< Keys.Length; i++){
			//'And'gate, only assesses as false if all keys are false
			if(Keys[i].getLocked()){
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Sets the collider to trigger, allowing the player to pass
	/// </summary>
	void open(){
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
	}

	/// <summary>
	/// Sets the collider to collider, making the object solid
	/// </summary>
	void close(){
		gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// 
	/// draws a line to all associated keys
	/// </summary>
	void OnDrawGizmos(){
		for(int i = 0; i < Keys.Length; i++){
			if(Keys[i] != null){
				Gizmos.color = Color.red;
				Gizmos.DrawLine(gameObject.transform.position, Keys[i].transform.position);
			}
		}
	}
}

﻿using UnityEngine;
using System.Collections;

public class GrappleControl : MonoBehaviour {

	/*grapple mechanics
	 * 
	 * what the grappling hook needs to do is:
	 * 	A: normally follow the player
	 *  B: when the grapple button is pressed (say g button or x on controller)
	 * 	   then it needs to go to where ever the cursor currently is (ie mouse)
	 * 	C: it needs to generate the rope (as a collection of individual rope
	 * 	   sprites while it is shooting towards to cursor.
	 * `D: if it runs into a wall before the cursor, it connects to it
	 * 	E: if it doesnt connect to a wall before it hits the cursor it will
	 * 	   keep going in a straight line till it hits a wall.
	 * 	F: if the cursor or any wall is out of its range, it will 
	 * 	   retract back to the player
	 * 	G: the player can then swing around at the distace of the grapple
	 * 	H: the player can also retract the rope to bring themselves closer to the grapple location
	 *  I: when the player is done, they can retract the rope, it detaches 
	 * 	   from the wall and winds back to the player.
	 * 
	 */
	public LayerMask notToHit;
	public float maxGrappleDisance = 100;
	public GameObject cursor;
	public GameObject grapple;
	public Vector3 mousePosition;
	
	//initialize the fire point for the gun
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		float mousex = Input.mousePosition.x;
		float mousey = Input.mousePosition.y;
		Vector3 firePointPos = new Vector3 (transform.position.x, transform.position.y, 10);
		mousePosition = cursor.transform.position;



		if (Input.GetButtonDown("Fire1")){
			Shoot(firePointPos);
		}
		
	}




	//shoot the grapple in the direction of the mouse
	void Shoot(Vector3 firePointPos){
		print (mousePosition);	

		GameObject clone;
		Quaternion zeroRotation = new Quaternion (0, 0, 0, 0);
		clone = Instantiate(grapple, transform.position, zeroRotation) as GameObject;
		clone.GetComponent<Rigidbody>().MovePosition(mousePosition * 10);

	}


}

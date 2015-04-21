using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	private const int FULL_GRAVITY = 2;
	private const int HALF_GRAVITY = 1;
	private const int ZERO_GRAVITY = 0;
	
	private int currentGravity = FULL_GRAVITY; //start on normal gravity
	
	private bool isJumping = false;

	public float jumpHeight = 5f;
	public float moveSpeed = 3f;



	void Update () {
		
		if(currentGravity == FULL_GRAVITY || currentGravity == HALF_GRAVITY)
			normalMovement ();
		else
			ZeroGravMovement();
	}


	//CURRENTLY ONLY CONTROLLING GRAVITY	
	void OnTriggerStay2D(Collider2D other){
		
		//All gravity zones in this game are defigned by a large invisable sprite that covers the entire 
		//area that uses the specific gravity in question.
		
		if(other.tag == "zeroGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale = 0;
			currentGravity = ZERO_GRAVITY;
			GetComponent<Rigidbody2D>().fixedAngle = false;
			Debug.Log("zero grav");
			
		} else if (other.tag == "halfGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale = 0.5f;
			currentGravity = HALF_GRAVITY;
			GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = true;
			Debug.Log("half grav");
			
		} else if (other.tag == "fullGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale= 1;
			currentGravity = FULL_GRAVITY;
			GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = true;
			Debug.Log("full grav");
		}
	}
	

	//NORMAL MOVEMENT CODE
	//
	//
	//
	//
	void normalMovement(){
		if(Input.GetKeyDown(KeyCode.W)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
		}	
		/*if(Input.GetKey(KeyCode.A)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
		}
		if(Input.GetKey(KeyCode.D)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
		}*/

		GetComponent<Transform>().Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,0,0);
	}
	
	//ZERO GRAVITY MOVEMENT CODE
	//
	//
	//
	//
	void ZeroGravMovement(){
		
		if(Input.GetKey(KeyCode.W))
		{
			GetComponent<Rigidbody2D>().AddRelativeForce (Vector2.up * 10);;
		}
		if(Input.GetKey(KeyCode.S))
		{
			GetComponent<Rigidbody2D>().AddRelativeForce (Vector2.up * -10);;
		}
		if(Input.GetKey(KeyCode.A))
		{
			GetComponent<Rigidbody2D>().AddRelativeForce (Vector2.right * -10);;
		}
		if(Input.GetKey(KeyCode.D))
		{
			GetComponent<Rigidbody2D>().AddRelativeForce (Vector2.right * 10);;
		}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			GetComponent<Rigidbody2D>().AddTorque(2f);
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<Rigidbody2D>().AddTorque(-2f);
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
	}
}



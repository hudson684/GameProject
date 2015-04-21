using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	
	private const int FULL_GRAVITY = 2;
	private const int HALF_GRAVITY = 1;
	private const int ZERO_GRAVITY = 0;
	
	private int currentGravity = FULL_GRAVITY; //start on normal gravity

	private bool isGrounded = true;
	
	public float jumpHeight = 7f;
	public float moveSpeed = 5f;
	public float jetpackSpeed = 5f;
	public float rotateSpeed = 2f;
	
	
	
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
			
		} else if (other.tag == "halfGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale = 0.5f;
			currentGravity = HALF_GRAVITY;
			GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = true;
			
		} else if (other.tag == "fullGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale= 1;
			currentGravity = FULL_GRAVITY;
			GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = true;
		}

		
	}

	//STOPPING DOUBLE JUMP
	//to stop double jumping, make sure that a jump can only happen when the Collider is touching a suitable object 
	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag == "Ground" || other.collider.tag == "Platform" || other.collider.tag == "Object")
			isGrounded = true;
	}



	
	
	//NORMAL MOVEMENT CODE
	//
	//
	//
	//
	void normalMovement(){
		if(Input.GetKeyDown(KeyCode.W) && isGrounded){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
			isGrounded = false;
		}
		
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
			GetComponent<Rigidbody2D>().AddForce (Vector2.up * jetpackSpeed);
		}
		if(Input.GetKey(KeyCode.S))
		{
			GetComponent<Rigidbody2D>().AddForce (Vector2.up * -jetpackSpeed);
		}
		if(Input.GetKey(KeyCode.A))
		{
			GetComponent<Rigidbody2D>().AddForce (Vector2.right * -jetpackSpeed);
		}
		if(Input.GetKey(KeyCode.D))
		{
			GetComponent<Rigidbody2D>().AddForce (Vector2.right * jetpackSpeed);
		}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			GetComponent<Rigidbody2D>().AddTorque(rotateSpeed);
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<Rigidbody2D>().AddTorque(-rotateSpeed);
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
	}
}

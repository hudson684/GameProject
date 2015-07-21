using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {



	//set the gravity 
	private const int FULL_GRAVITY = 2;
	private const int ZERO_GRAVITY = 0;

	private int currentGravity = FULL_GRAVITY; //start on normal gravity

	//initialize the private variables used in the code
	private bool isGrounded = true;
	private bool isGrappling = false;
	private bool isCrouched = false;
	private bool isMantling = false;
	private bool isLeft = false;

	//the objects that the player will need to interact with
	private GameObject deathNodeObject;
	private DeathNode deathNode;

	private GameObject CheckPointMarker;
	private Grounded groundedCode;


	//public variables
	public float jumpHeight = 7f;
	public float moveSpeed = 5f;
	public float jetpackSpeed = 5f;
	public float rotateSpeed = 2f;
	public float grappleSpeed = 10f;
	public float fallDamageAt = -25.0f;
	public float normalGravValue = 5.0f;

	//crouched variables
	private BoxCollider2D box;
	Vector2 fullSize;
	Vector2 crouchedSize;
	Vector2 fullOffset;
	Vector2 crouchedOffset;

	//rotation variables
	private Quaternion rotation = Quaternion.identity;

	
	void Start(){
		//find the checkpoint marker and send the player to it
		CheckPointMarker = GameObject.FindGameObjectWithTag ("CheckPointMarker");
		this.transform.position = CheckPointMarker.transform.position;

		//initialize the trigger that checks to see if the player is grounded
		groundedCode = (Grounded)this.GetComponentInChildren(typeof(Grounded));
	
		//make sure the player doesnt rotate
		GetComponent<Rigidbody2D>().fixedAngle = true;
	
		//initialize contact with the deathnode
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponent(typeof(DeathNode));



		//CROUCHING SETUP
		//the setup for the size and ofsets for the crouching
		box = this.GetComponent<BoxCollider2D>();
		fullSize = box.size;
		crouchedSize = new Vector2(box.size.x, box.size.y - 0.5f);
		fullOffset = box.offset;
		crouchedOffset = new Vector2 (box.offset.x, 0.5f);

	}



	//WHILE THE PLAYER IS NOT DEAD, MOVE THE PLAYER UNDER THREE CATAGORIES; GRAPPLING, NORMAL GRAV AND ZERO GRAV
	void Update () {
		if (!deathNode.getDeath()) {
			if (!isGrappling) {
				if (currentGravity == FULL_GRAVITY)
					normalMovement ();
				else
					ZeroGravMovement ();
			} else {
				grapleMovement ();
			}	
		}
	}
	
	
	//GRAVITY SETTERS	
	void OnTriggerStay2D(Collider2D other){
		
		//All gravity zones in this game are defigned by a large invisable sprite that covers the entire 
		//area that uses the specific gravity in question.
		
		if(other.tag == "zeroGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale = 0;
			currentGravity = ZERO_GRAVITY;
			GetComponent<Rigidbody2D>().fixedAngle = false;
			
		} else if (other.tag == "fullGravZone"){
			
			GetComponent<Rigidbody2D>().gravityScale= normalGravValue;
			currentGravity = FULL_GRAVITY;
			GetComponent<Rigidbody2D>().rotation = 0f;
			GetComponent<Rigidbody2D>().fixedAngle = true;
		}
		
		
	}
	
	//fall damage
	//the fall damage is set on the y colision (up and down) to be at the value the developer sets
	//it also wont detect the grapple objects, thought it shouldnt in the first place (just in case)
	void OnCollisionEnter2D(Collision2D other){
		if (other.relativeVelocity.y < fallDamageAt && other.collider.tag != "Grapple"){
			Debug.Log ("dead at: " + other.relativeVelocity.y.ToString ());
			Debug.Log ("Killed by: " + other.collider.tag.ToString());
			deathNode.setDeath(true);
		}

	}
	
	
	
	
	
	//NORMAL MOVEMENT CODE
	//
	// SPACE: up
	// A: Left
	// D: Right
	// LEFT CTRL: Crouch
	void normalMovement(){
		if(Input.GetKeyDown(KeyCode.Space) && groundedCode.getGrounded()){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
		}

		if (Input.GetKey(KeyCode.A)) {
			GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime,0,0);
			isLeft = true;
		} 
		
		if (Input.GetKey(KeyCode.D)) {
			GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime,0,0);
			isLeft = false;
		}


		if (Input.GetKeyDown(KeyCode.LeftControl)){
			isCrouched = !isCrouched;

			if(isCrouched){
				moveSpeed = moveSpeed/2;
				box.size = crouchedSize;
				box.offset = crouchedOffset;

			} else {
				moveSpeed = moveSpeed*2;
				box.size = fullSize;
				box.offset = fullOffset;
			}
		}



		//CODE BY LIAM, rotates player left/right (note has to be seperate to normal a/d control because this only needs
		//to happen once per pressing
		if(Input.GetKeyDown("a")){
			rotation.eulerAngles = new Vector2(0,180);
			transform.rotation = rotation;
		}
		if(Input.GetKeyDown("d")){
			rotation.eulerAngles = new Vector2(0,0);
			transform.rotation = rotation;
		}

	}
	
	//ZERO GRAVITY MOVEMENT CODE
	//
	// W: up
	// S: down
	// A: left
	// W: right
	// Q: rotate left
	// E: rotate right
	// SPACE: stop rotation
	//
	void ZeroGravMovement(){
		
		if(Input.GetKey(KeyCode.W))
		{

			Debug.Log("should add up force");
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
		if(Input.GetKey(KeyCode.Q))
		{
			GetComponent<Rigidbody2D>().AddTorque(rotateSpeed);
			Debug.Log("adding torque");
		}
		if(Input.GetKey(KeyCode.E))
		{
			GetComponent<Rigidbody2D>().AddTorque(-rotateSpeed);
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
	}



	//GRAPPLE MOVEMENT,
	//
	// SPACE: reduce number of links
	// LEFT CTRL: add links
	//
	// FULL GRAV CODE AND ZERO GRAV CODE AS ABOVE WITH NO JUMP/CROUCH
	//
	public void grapleMovement(){
		
		GrappleControl grapple = (GrappleControl)this.GetComponentInChildren (typeof(GrappleControl));
		
		if (Input.GetKey(KeyCode.Space)) {
			grapple.reduce ();
		}
		if (Input.GetKey(KeyCode.LeftControl)) {
			grapple.add ();
		}
		
		if (currentGravity == FULL_GRAVITY) {
			if (Input.GetKey (KeyCode.A)) {
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -grappleSpeed);
			}
			if (Input.GetKey (KeyCode.D)) {
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * grappleSpeed);
			}
		} else {
			
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

	//GETTERS AND SETTERS
	//GROUNDED

	public bool getGrounded(){
		return groundedCode.getGrounded();
	}

	public void setGrounded(bool grounded){
		isGrounded = grounded;
	}

	//GRAPPLING
	public bool getIsGrappling(){
		return isGrappling;
	}

	public void setGrappling(bool grappling){
		isGrappling = grappling;
	}
	//MANTLE
	public bool getMantle(){
		
		return isMantling;
	}

	public void setMantling(bool mantling){
		isMantling = mantling;
	}

	//CROUCHED
	public void setIsCrouched(bool crouched){

		isCrouched = crouched;
	}


	public bool getIsCrouched(){

		return isCrouched;
	}


	//CODE BY LIAM, CHECKS TO SEE ORIENTATION OF PLAYER
	public bool getLeft(){
		return isLeft;
	}

}

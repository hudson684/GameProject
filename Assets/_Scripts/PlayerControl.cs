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
	private ControlNode contNode;

	private GameObject CheckPointMarker;
	private Grounded groundedCode;

	private GameObject heldObject;
	private bool isHolding = false;
	private string tempHeldLayer = " ";

	//public variables
	public float jumpHeight = 7f;
	public float moveSpeed = 5f;
	public float jetpackSpeed = 5f;
	public float rotateSpeed = 2f;
	public float grappleSpeed = 10f;
	public float fallDamageAt = -25.0f;
	public float normalGravValue = 2.0f;

	//crouched variables
	private BoxCollider2D box;
	Vector2 fullSize;
	Vector2 crouchedSize;
	Vector2 fullOffset;
	Vector2 crouchedOffset;
	bool shouldCrouch = false;
	public float crouchDis = 0.2f;
	public LayerMask crouchMask;

	//animation variables
	private Animator anim;
	private AudioSource audioController;
	public AudioClip[] clips;
	private AudioClip currentClip;
	private int jumpHash = Animator.StringToHash("Jump");
	private int groundHash = Animator.StringToHash("Grounded");
	private int speedHash = Animator.StringToHash("Speed");
	private int crouchHash = Animator.StringToHash("Crouched");
	private int Grappling = Animator.StringToHash("Grappling");
	private int DeathHash = Animator.StringToHash("Death");
	private int interactHash = Animator.StringToHash("Interact");
	private int Mantleing = Animator.StringToHash ("Mantleing");
	private int gravHash = Animator.StringToHash("Gravity");
	private bool crouched = false;

	//mantle Variables
	private Mantle mantleComponent;


	//rotation variables
	private Quaternion rotation = Quaternion.identity;

	
	void Awake(){
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
		contNode = (ControlNode) deathNodeObject.GetComponent(typeof(ControlNode));

		anim = GetComponent<Animator>();
		audioController = GetComponent<AudioSource>();


		//CROUCHING SETUP
		//the setup for the size and ofsets for the crouching
		box = this.GetComponent<BoxCollider2D>();
		fullSize = box.size;
		crouchedSize = new Vector2(0.7546405f, 1.071778f);
		fullOffset = box.offset;
		crouchedOffset = new Vector2(0.1905554f, 0.5293568f);

		contNode.setPlayerPosition (this.transform.position);


		//Mantle Setup
		mantleComponent = GetComponentInChildren<Mantle>();

	}



	//WHILE THE PLAYER IS NOT DEAD, MOVE THE PLAYER UNDER THREE CATAGORIES; GRAPPLING, NORMAL GRAV AND ZERO GRAV
	void Update () {

		//Debug.Log ("Is holding on: " + isHolding.ToString());

		CurrentSetting ();

		contNode.setPlayerPosition (this.transform.position);
		contNode.setPlayerFacingLeft (isLeft);


		if (!deathNode.getDeath()) {
			if (!isGrappling) {
				if (currentGravity == FULL_GRAVITY){
					normalMovement ();
				}else{
					if(contNode.getZeroGrav()){
						ZeroGravMovement();
					}
				}
			} else {
				grapleMovement ();
			}	
		}


		if (isHolding) {
			keepHoldingOn();
		}

		//float move = Mathf.Abs(Input.GetAxis("Horizontal"));
		
		/*	if(Input.GetKeyDown(KeyCode.Space)){
				anim.SetTrigger (jumpHash);
			} */
		
		if(Input.GetKeyDown(KeyCode.F)){
			anim.SetTrigger(interactHash);
		}
		
		
		//anim.SetFloat(speedHash,move);
		anim.SetBool(groundHash, isGrounded);
		anim.SetBool(Grappling, isGrappling);
		//anim.SetBool(Mantleing, isMantling);
		//anim.SetBool(crouchHash, isCrouched);
		anim.SetBool(DeathHash, deathNode.getDeath());


	}

	void footFall(){
		
		currentClip = clips[Random.Range(0,2)];
		audioController.clip = currentClip;
		audioController.Play();
	}


	//temporary holders 
	void CurrentSetting(){
		isGrounded = groundedCode.getGrounded ();
	}
	
	
	//GRAVITY SETTERS	
	void OnTriggerStay2D(Collider2D other){
		
		//All gravity zones in this game are defigned by a large invisable sprite that covers the entire 
		//area that uses the specific gravity in question.
		
		if(other.tag == "zeroGravZone"){
			anim.SetBool(gravHash,false);
			GetComponent<Rigidbody2D>().gravityScale = 0;
			currentGravity = ZERO_GRAVITY;
			GetComponent<Rigidbody2D>().fixedAngle = false;
			
		} else if (other.tag == "fullGravZone"){
			anim.SetBool(gravHash,true);
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
			deathNode.setDeath(true);
		}



	}

	void OnCollisionStay2D(Collision2D other){
		if (!isHolding) {
			if(other.collider.tag == "Object"){
				if(Input.GetButtonDown("Interact")){

					GrappleControl grapple = (GrappleControl)this.GetComponentInChildren (typeof(GrappleControl));
					
					if(isGrappling){
						if(other.collider.name == grapple.getObjName()){
							grapple.retractGrapple();
							
							startHoldingOn(other.gameObject);
						}
						
					} else {

						startHoldingOn(other.gameObject);
					}

					
				}
			}
		}


	}
	

	private void startHoldingOn(GameObject obj){
		if (isHolding) {
			stopHoldingOn();
		}

		obj.transform.SetParent (this.transform);
		heldObject = obj;
		heldObject.GetComponent<Rigidbody2D> ().Sleep ();
		//tempHeldLayer = heldObject.tag;
		//heldObject.tag = "Player";
		//heldObject.layer = LayerMask.NameToLayer ("Player");
		StartCoroutine ("holdWait");

	}


	IEnumerator holdWait(){

		yield return new WaitForSeconds (0.2f);
		isHolding = true;
	}

	private void keepHoldingOn(){

		//float disX = 1f + ((heldObject.transform.lossyScale.x -1f)/ 3f);

		float disX = 1f + heldObject.GetComponent<BoxCollider2D> ().size.x;
		//float disY = 1f + ((heldObject.transform.lossyScale.y -1f)/ 3f);
		float disY = 1f + heldObject.GetComponent<BoxCollider2D> ().size.y;

		if (heldObject != null) {
			heldObject.transform.localPosition = new Vector3 (disX, disY, 0f);
		}

		if (isHolding) {
			if(Input.GetButtonDown("Interact"))
			{
				stopHoldingOn();
			}
		}


	}

	private void stopHoldingOn(){
		//heldObject.tag = tempHeldLayer;
		//heldObject.layer = LayerMask.NameToLayer ("Object");
		heldObject.transform.SetParent (null);
		heldObject.GetComponent<Rigidbody2D> ().WakeUp ();
		heldObject = null;
		//StartCoroutine ("holdWait");
		isHolding = false;
	}

	//NORMAL MOVEMENT CODE
	//
	// SPACE: up
	// A: Left
	// D: Right
	// LEFT CTRL: Crouch
	void normalMovement(){


		//if (isHolding) {

		//	stopHoldingOn();
		//}


		if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isMantling){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
			anim.SetTrigger(jumpHash);
		}

		if (Input.GetKey(KeyCode.A)) {
			GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime,0,0);
			anim.SetFloat(speedHash,1f);
			isLeft = true;
		} else if (Input.GetKey(KeyCode.D)) {
			GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime,0,0);
			anim.SetFloat(speedHash,-1f);
			isLeft = false;
		}else{
			anim.SetFloat(speedHash,0f);
		}

		
		//CODE BY LIAM, rotates player left/right (note has to be seperate to normal a/d control because this only needs
		//to happen once per pressing
		if(Input.GetKeyDown("a") && !Input.GetKeyDown(KeyCode.D)){
			isLeft = true;
			rotation.eulerAngles = new Vector2(0,180);
			transform.rotation = rotation;
		} 

		if(Input.GetKeyDown("d") && !Input.GetKeyDown(KeyCode.A)){
			isLeft = false;
			rotation.eulerAngles = new Vector2(0,0);
			transform.rotation = rotation;
		}


		if (Input.GetButtonDown("Crouch") || Input.GetKeyDown(KeyCode.C)){
			isCrouched = !isCrouched;
			if(isCrouched){
				crouch();
			} else {
				if(!shouldCrouch){
					uncrouch();
				}
			}
		}

		RaycastHit2D hit = Physics2D.Raycast (new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, this.transform.position.z), new Vector3 (0f, 1f, 0f), crouchDis, crouchMask);


		if (hit.collider != null) {
			Debug.Log ("should crouch");
			Debug.Log(hit.collider.name.ToString());


			if(!isCrouched){
				shouldCrouch = true;
				isCrouched = true;
				crouch ();
			}

		} else {

			shouldCrouch = false;
		}

	}

	void OnDrawGizmos(){
		Gizmos.DrawRay (new Vector3 (this.transform.position.x, this.transform.position.y + 0.2f, this.transform.position.z), new Vector3 (0f, 1f, 0f));
	}

	void crouch(){
		moveSpeed = 3f;
		box.size = crouchedSize;
		box.offset = crouchedOffset;
		anim.SetBool(crouchHash,isCrouched);
		
	} 
	
	void uncrouch(){
		moveSpeed = 6f;
		box.size = fullSize;
		box.offset = fullOffset;
		anim.SetBool(crouchHash, isCrouched);
	}
	
	
	int doubleTapCounter = 0;
	float doubleTapTimer = 0.5f;
	
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
		if (Input.GetButton("Crouch") || Input.GetKey(KeyCode.C)) {
			grapple.add ();
		}
		
		if (currentGravity == FULL_GRAVITY) {

			if(isGrounded){
				if (Input.GetKey(KeyCode.A)) {
					GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime,0,0);
					isLeft = true;
				} 
				
				if (Input.GetKey(KeyCode.D)) {
					GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime,0,0);
					isLeft = false;
				}

				if(Input.GetKeyDown("a")){
					rotation.eulerAngles = new Vector2(0,180);
					transform.rotation = rotation;
				}
				if(Input.GetKeyDown("d")){
					rotation.eulerAngles = new Vector2(0,0);
					transform.rotation = rotation;
				}
			} else {
				if (Input.GetKey (KeyCode.A)) {
					GetComponent<Rigidbody2D> ().AddForce (Vector2.right * -grappleSpeed);
				}
				if (Input.GetKey (KeyCode.D)) {
					GetComponent<Rigidbody2D> ().AddForce (Vector2.right * grappleSpeed);
				}
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

	public void triggerMantleUp(){
		mantleComponent.mantleUp();
	}

}

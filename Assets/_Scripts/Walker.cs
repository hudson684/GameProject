using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public Vector3[] patrolPoints;
	private int currentPoint = 0;

	private Transform[] hitBarrier;
	public LayerMask toHit;

	public Material alert;
	public Material normal;
	
	private DynamicLight light2D;
	
	private bool triggered = false;
	private bool patroling = true;
	
	public float Speed = 10f;
	private float distance = 0.1f;
	public bool loop = false;

	
	public float fauxGravity = -50f;

	int[,,] indexis;


	//the x axis of the RotTable
	private const int LEFT = 0;
	private const int RIGHT = 1;
	private const int UP = 2;
	private const int DOWN = 3;

	//the y axis for the RotTable
	private const int DOWNGRAV = 0;
	private const int RIGHTGRAV = 1;
	private const int UPGRAV = 2;
	private const int LEFTGRAV = 3;

	private const int ROTATION = 0;
	private const int ROTATIONINDEX = 1;
	private const int GRAVITYINDEX = 2;


	private Transform bodyControl;

	private int curRotInt;
	private int curDownInt;

	private Vector3 currentRotation;
	private Vector3 jumpChecker;
	private Vector2 downWardsForce;





	void Awake(){

		light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
		light2D.setMainMaterial(normal);
		
		//finds all the childern of the patroler (for use in player detection);
		hitBarrier = transform.GetComponentsInChildren<Transform>();

		bodyControl = transform.FindChild ("root");

		this.transform.position = patrolPoints [0];

		curRotInt = 1;
		curDownInt = 0;

		UpdateRotation (curRotInt);
		UpdateDownwardsForce (curDownInt);


		indexis = new int[4, 4, 3] {
			//Left <-             Right ->             up ^                down v 
			{{90, UP ,LEFTGRAV},  {-90,UP,RIGHTGRAV},  {0,-1,-1},          {0,-1,-1}},            //down grav
			{{0,-1,-1},           {0,-1,-1},           {-90,LEFT ,UPGRAV}, {90,LEFT,DOWNGRAV}},   //right grav
			{{-90,DOWN,LEFTGRAV}, {90,DOWN ,RIGHTGRAV},{0,-1,-1},          {0,-1,-1}},            //up grav
			{{0,-1,-1},           {0,-1,-1},           {90,RIGHT,UPGRAV},  {-90,RIGHT ,DOWNGRAV}} //left grav
		};
	}

	void Update(){
		                                                       
		if (!collisionAhead()) {

			Vector3 velocity = Vector3.zero;

			if (currentPoint < patrolPoints.Length) {
				Vector3 nextPoint = patrolPoints [currentPoint];
				Vector3 moveDirection = nextPoint - this.transform.position;

				velocity = this.GetComponent<Rigidbody2D> ().velocity;

				if (moveDirection.magnitude < distance) {
					currentPoint++;
				} else {
					velocity = moveDirection.normalized * Speed;
				} 
			} else {
				if (loop) {
					currentPoint = 0;
				} else {
					velocity = Vector3.zero;
				}
			}

			this.GetComponent<Rigidbody2D> ().velocity = velocity;
		} else {

			if(shouldNotJump()){
				//rotate to the new axis
				Debug.Log("Shouldn't Jump");
				this.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
				rotateWalker();


			} else {
				Debug.Log("Should Jump");

				if(curDownInt == DOWNGRAV || curDownInt == UPGRAV){ 

				this.GetComponent<Rigidbody2D>().velocity = new Vector3(jumpChecker.x, jumpChecker.y * 50f);

				} else {

					this.GetComponent<Rigidbody2D>().velocity = new Vector3(jumpChecker.x * 50f, jumpChecker.y);
				}
				                                                    

			}

		}


		this.GetComponent<Rigidbody2D> ().velocity += downWardsForce;
		Debug.Log ("applied downwards force: " + downWardsForce.ToString());
	}

	private void rotateWalker(){
		//the table below is the indexis table defigned in the awake function.
		//
		//when the walker comes to an wall that it cannot jump over it has to rotate
		//this is represented by a table along with indexis of what the new values for
		//the current rotation index and the new downwards force.
		//
		//for example indexis[2,1,0] is the rotation that happens the walker is is rotated to the left and walkig up.
		//this is simplified by making constants so the actual expression would be
		//indexis[LEFT, UPGRAV, ROTATION]
		//
		//                 facing:
		// Angle           left      right      up          down
		// 0    (downgrav) 90, 2,3   -90,2,1    0,-1,-1   	0,-1,-1
		// 90   (leftgrav) 0,-1,-1   0,-1,-1    90,1, 2     -90, 1, 0
		// 180    (upGrav) -90,3,3   90,3, 1    0,-1,-1 	0,-1,-1
		// 270 (rightgrab) 0,-1,-1   0,-1,-1    -90,0,2     90,0, 0


		//chaging the rotation of the child object

		Debug.Log ("should be trying to acess rotInt: " + curRotInt + " downInt: " + curDownInt + " and section: "  + ROTATION );

		bodyControl.Rotate (new Vector3(indexis[curDownInt, curRotInt, ROTATION], 0, 0));

		UpdateRotation (indexis [curDownInt, curRotInt, ROTATIONINDEX]);
		UpdateDownwardsForce (indexis [curDownInt, curRotInt, GRAVITYINDEX]);

		int tempRotInt = curRotInt;
		
		curRotInt = indexis [curDownInt, curRotInt, ROTATIONINDEX];
		Debug.Log ("Changed Rotation index to : " + curRotInt);
		
		curDownInt = indexis [curDownInt, tempRotInt, GRAVITYINDEX];
		Debug.Log ("Changed Downwards index to : " + curDownInt);

		Debug.Log ("Jump Checker: " + jumpChecker.ToString());


	}


	private void UpdateRotation(int i){

		switch (i) {
		case LEFT: currentRotation = -transform.right; jumpChecker.x = -3.0f; break;
		case RIGHT: currentRotation = transform.right; jumpChecker.x =  3.0f; break;
		case UP: currentRotation = transform.up; jumpChecker.y = 4.0f; break;
		case DOWN: currentRotation = -transform.up; jumpChecker.y = -4.0f; break;

		}
	}

	
	private void UpdateDownwardsForce(int i){
		
		switch (i) {
		case DOWNGRAV: downWardsForce = new Vector2(0, -fauxGravity); jumpChecker.y = 4.0f; break;
		case UPGRAV: downWardsForce = new Vector2(0, fauxGravity); jumpChecker.y = -4.0f; break;
		case LEFTGRAV: downWardsForce = new Vector2(-fauxGravity, 0); jumpChecker.x = 3.0f; break;
		case RIGHTGRAV: downWardsForce = new Vector2(fauxGravity, 0); jumpChecker.x = -3.0f; break;	
		}
	}

	private bool shouldNotJump(){

		RaycastHit2D detection = Physics2D.Raycast (this.transform.position, jumpChecker, 5f, toHit);
		
		
		if (detection.collider != null) {
			return true;
		} else {
			return false;
		}


	}

	//detecting to see if there is an object right infront of the walker;
	private bool collisionAhead(){

		RaycastHit2D detection = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y + 1f)
		                                            , currentRotation, 3f, toHit);


		if (detection.collider != null) {
		return true;
		} else {
			return false;
		}
	}
	//Development code
	void OnDrawGizmos(){

		Ray rayT = new Ray(new Vector3 (this.transform.position.x, this.transform.position.y + 1f,0f),  currentRotation);

		Gizmos.DrawRay (rayT);
		Gizmos.DrawRay(this.transform.position, jumpChecker);


		for (int i = 1; i < patrolPoints.Length; i++) {
			Gizmos.DrawLine(patrolPoints[i-1],patrolPoints[i]);
		}


		if (loop) {
			Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1], patrolPoints[0]);
		}

	}


	
	//checks to see if the player has been detected by its array of nodes
	private bool foundPlayer(){
		for(int i = 0; i < (hitBarrier.Length -1); i++){
			if(hitBarrier[i].tag == "PatrolViewNodes"){
				float distance = Vector3.Distance(this.transform.position, hitBarrier[i].position);
				
				RaycastHit2D hit = Physics2D.Raycast (this.transform.position, (hitBarrier[i].position - this.transform.position), distance, toHit);
				
				if(hit.collider != null){
					if(hit.collider.tag == "Player"){
						return true;
					} 
				} 
			}
		}
		return false;
	}

}

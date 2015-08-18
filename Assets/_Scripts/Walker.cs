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
	private bool stopped = false;

	private GameObject player;
	
	public float Speed = 10f;
	public float GoalMargine = 0.1f;
	private float slowSpeedForward = 2f;
	public bool loop = false;
	public float jumpFactor = 5f;

	/// <summary>
	/// The minimum drop that is needed to decide to rotate the oposite direction 
	/// for cliffs
	/// </summary>
	public float canyionDistance = 10f;

	
	public float fauxGravity = -50f;

	int[,,] indexis;
	int[,,] reverseIndexis;


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

	private const float forwardSpeedMod = 2f;


	private Transform bodyControl;

	private int curRotInt;
	private int curDownInt;

	private Vector3 currentRotation;
	private Vector3 jumpChecker;
	private Vector2 downWardsForce;
	private Vector3 downDirection;

	private BoxCollider2D col;

	public float chasePlayerTime = 30f;

	private bool rotated = false;

	private GameObject deathNodeObject;
	private DeathNode deathNode;
	
	void Awake(){

		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponentInParent(typeof(DeathNode));


		col = this.GetComponent<BoxCollider2D> ();

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
			{{0,-1,-1},           {0,-1,-1},           {90,RIGHT,UPGRAV},  {-90,RIGHT,DOWNGRAV}} //left grav
		};


		/*indexis = new int[4, 4, 3] {
			//<- -> ^  v 
			{-1, 1, 0, 0},     //down grav
			{0,  0, 1,-1},     //right grav
			{1,  1, 0, 0},     //up grav
			{90, -90, 0, 0},     //left grav
		};*/


		reverseIndexis = new int[4, 4, 3] {
			//Left <-             Right ->             up ^                down v 
			{{-90, DOWN ,RIGHTGRAV},{90,DOWN,LEFTGRAV},  {0,-1,-1},            {0,-1,-1}},            //down grav
			{{0,-1,-1},             {0,-1,-1},           {90,RIGHT ,DOWNGRAV}, {-90,RIGHT, UPGRAV}},   //right grav
			{{90,UP,RIGHTGRAV},     {-90,UP ,LEFTGRAV},  {0,-1,-1},            {0,-1,-1}},            //up grav
			{{0,-1,-1},             {0,-1,-1},           {-90,LEFT,DOWNGRAV},  {90,LEFT, UPGRAV}} //left grav
		};
	}

	void Update(){
		//Debug.Log ("rotate oposite check: " + shouldNotTurnBackwards().ToString());

		if (!triggered) {
			triggered = foundPlayer();
			StartCoroutine("countDown");
		}

		stopped = checkStoppedTest ();
		                                                       
		if (!collisionAhead () && shouldNotTurnBackwards ()) {

			rotated = false;

			Vector3 velocity = Vector3.zero;


			if (currentPoint < patrolPoints.Length) {
				Vector3 nextPoint;
				if(!triggered){
					nextPoint = patrolPoints [currentPoint];
				} else {
					nextPoint = player.transform.position;
					playerKillZone(); 
				}

				//Debug.Log("next point: " + nextPoint.ToString());
				//Debug.Log("current position: " + transform.position.ToString());

				Vector3 moveDirection = nextPoint - this.transform.position;

				velocity = this.GetComponent<Rigidbody2D> ().velocity;

				shouldFlip(moveDirection);

				if (moveDirection.magnitude < GoalMargine) {
					currentPoint++;
				} else {
					velocity = moveDirection.normalized * Speed;

					//Debug.Log("Move Direction: " + moveDirection.normalized * Speed);
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

			if (shouldNotJump ()) {
				//rotate to the new axis
				//Debug.Log("Shouldn't Jump");

				if (!rotated) {
					rotated = true;
					rotateWalker ();
				}


			} else if (collisionAhead ()) {
				//Debug.Log("Should Jump");

				//rotated = true; //TODO: replace this with a better bool
				if (curDownInt == DOWNGRAV || curDownInt == UPGRAV) { 

					this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (jumpChecker.x, jumpChecker.y * jumpFactor,0f);

				} else {

					this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (jumpChecker.x * jumpFactor, jumpChecker.y,0f);
				}
			                                                    

			} else if (!shouldNotTurnBackwards ()) {

				if (!rotated) {
					rotated = true;
					rotateWalkerOposite ();
				}

			}// else if (reverseFlip ()) {
			//	this.transform.Rotate (180f, 0, 0);
			//}

		}

		this.GetComponent<Rigidbody2D> ().velocity += downWardsForce;
		//Debug.Log ("applied downwards force: " + downWardsForce.ToString());



		RaycastHit2D detection = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y ), downDirection, 2f, toHit);

		//Debug.Log("Current Speed of x: " + this.GetComponent<Rigidbody2D> ().velocity.x.ToString ());
		//Debug.Log("Current Speed of y: " + this.GetComponent<Rigidbody2D> ().velocity.y.ToString ());



		if (detection.collider != null && stopped) {
			stopped = false;
			moveForward ();
			//Debug.Log("try and move forward");
		}
	}


	private void shouldFlip(Vector3 direction){
		if (curRotInt == RIGHT) {
			if(direction.x < 0f){
				bodyControl.Rotate(new Vector3(0f, 180f, 0f));
				UpdateRotation(LEFT);
				curRotInt = LEFT;
			}
		} else if (curRotInt == LEFT) {
			if(direction.x > 0f){
				bodyControl.Rotate(new Vector3(0f, 180f, 0f));
				UpdateRotation(RIGHT);
				curRotInt = RIGHT;
			}

		} else if (curRotInt == UP) {
			if(direction.y > 0f){
				bodyControl.Rotate(new Vector3(180f, 0f, 0f));
				UpdateRotation(DOWN);
				curRotInt = DOWN;
			}

		} else if (curRotInt == DOWN) {
			if(direction.y < 0f){
				bodyControl.Rotate(new Vector3(180f, 0f, 0f));
				UpdateRotation(UP);
				curRotInt = UP;
			}

		}

	}
	

	private IEnumerator countDown(){


		yield return new WaitForSeconds (chasePlayerTime);
		triggered = false;
	}

	private bool checkStoppedTest(){
		
		
		//Debug.Log("Current Speed of x: " + this.GetComponent<Rigidbody2D> ().velocity.x.ToString ());
		//Debug.Log("Current Speed of y: " + this.GetComponent<Rigidbody2D> ().velocity.y.ToString ());
		//Debug.Log ("Current Rotation: " + curRotInt.ToString());
		
		if (this.GetComponent<Rigidbody2D> ().velocity.x >= -slowSpeedForward && curRotInt == LEFT) {
			return true;
		} else if (this.GetComponent<Rigidbody2D> ().velocity.x <= slowSpeedForward && curRotInt == RIGHT) {
			return true;
		} else if (this.GetComponent<Rigidbody2D> ().velocity.y <= slowSpeedForward && curRotInt == UP) {
			return true;
		} else if (this.GetComponent<Rigidbody2D> ().velocity.y >= -slowSpeedForward && curRotInt == DOWN) {
			return true;
		}

		return false;
		
	}

	private void moveForward(){

		
		//Debug.Log("Current Speed of x: " + this.GetComponent<Rigidbody2D> ().velocity.x.ToString ());
		//Debug.Log("Current Speed of y: " + this.GetComponent<Rigidbody2D> ().velocity.y.ToString ());
		//Debug.Log ("Current Rotation: " + curRotInt.ToString());

		if (curRotInt == LEFT) {
			this.GetComponent<Rigidbody2D> ().velocity += new Vector2 (-Speed * forwardSpeedMod, 0f);

		} else if (curRotInt == RIGHT) {
			this.GetComponent<Rigidbody2D> ().velocity += new Vector2 (Speed * forwardSpeedMod, 0f);
		
		} else if (curRotInt == UP) {
			this.GetComponent<Rigidbody2D> ().velocity += new Vector2 (0f, Speed * forwardSpeedMod);

		} else if (curRotInt == DOWN) {
			this.GetComponent<Rigidbody2D> ().velocity += new Vector2 (0f, -Speed * forwardSpeedMod);
		} 

	}

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
	// 0    (downgrav) 90, 2,3   -90,2,1    0,-1,-1   	0,-1,-1    (x rotation, direction, gravity)
	// 90   (leftgrav) 0,-1,-1   0,-1,-1    90,1, 2     -90, 1, 0
	// 180    (upGrav) -90,3,3   90,3, 1    0,-1,-1 	0,-1,-1
	// 270 (rightgrab) 0,-1,-1   0,-1,-1    -90,0,2     90,0, 0

	
	
	//chaging the rotation of the child object

	private void rotateWalker(){
		//Debug.Log ("normal rotation");
		//Debug.Log ("should be trying to acess rotInt: " + curRotInt + " downInt: " + curDownInt + " and section: "  + ROTATION );

		bodyControl.Rotate (new Vector3(indexis[curDownInt, curRotInt, ROTATION], 0, 0));

		UpdateRotation (indexis [curDownInt, curRotInt, ROTATIONINDEX]);
		UpdateDownwardsForce (indexis [curDownInt, curRotInt, GRAVITYINDEX]);

		int tempRotInt = curRotInt;
		
		curRotInt = indexis [curDownInt, curRotInt, ROTATIONINDEX];
		//Debug.Log ("Changed Rotation index to : " + curRotInt);
		
		curDownInt = indexis [curDownInt, tempRotInt, GRAVITYINDEX];
		//Debug.Log ("Changed Downwards index to : " + curDownInt);

		//Debug.Log ("Jump Checker: " + jumpChecker.ToString());


	}



	//the table below is the indexis table defigned in the awake function.
	//
	//when the walker comes to an end of a wath and then there is a 270 degree turn to a steep drop off, the
	//walker will rotate down towards the next edge 
	//the x axis of the RotTable
	//private const int LEFT = 0;
	//private const int RIGHT = 1;
	//private const int UP = 2;
	//private const int DOWN = 3;
	
	//the y axis for the RotTable
	//private const int DOWNGRAV = 0;
	//private const int RIGHTGRAV = 1;
	//private const int UPGRAV = 2;
	//private const int LEFTGRAV = 3;
	//
	//                 facing:
	// Angle           left      right      up          down
	// 0    (downgrav) -90,3,1   90,3, 3    0,-1,-1   	0,-1,-1  (rotation, direction, gravity)
	// 90   (leftgrav) 0,-1,-1   0,-1,-1    -90,0,0     90, 0, 2
	// 180    (upGrav) 90,2, 1   -90,2,3    0,-1,-1 	0,-1,-1
	// 270 (rightgrab) 0,-1,-1   0,-1,-1    90,1,0     -90,1, 2
	
	
	
	//chaging the rotation of the child object
	
	private void rotateWalkerOposite(){

		//Debug.Log ("backwards rotation:");
		
		//Debug.Log ("should be rotating backwards");
		bodyControl.Rotate (new Vector3(reverseIndexis[curDownInt, curRotInt, ROTATION], 0, 0));
		
		UpdateRotation (reverseIndexis [curDownInt, curRotInt, ROTATIONINDEX]);
		UpdateDownwardsForce (reverseIndexis [curDownInt, curRotInt, GRAVITYINDEX]);

		//Debug.Log ("Rotated : " + reverseIndexis [curDownInt, curRotInt, ROTATION] + "Towards gravity: " + reverseIndexis [curDownInt, curRotInt, GRAVITYINDEX]);
		
		int tempRotInt = curRotInt;
		
		curRotInt = reverseIndexis [curDownInt, curRotInt, ROTATIONINDEX];
		//Debug.Log ("Changed Rotation index to : " + curRotInt);
		
		curDownInt = reverseIndexis [curDownInt, tempRotInt, GRAVITYINDEX];
		//Debug.Log ("Changed Downwards index to : " + curDownInt);
		
		//Debug.Log ("Jump Checker: " + jumpChecker.ToString());
		
		
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

		if (i == DOWNGRAV) {
			downWardsForce = new Vector2(0, -fauxGravity);
			jumpChecker.y = 4.0f; 
			downDirection = new Vector2 (0f, -1f); 
			col.offset = new Vector2(0f, 0.03f);
			col.size = new Vector2(0.11f, 0.06f);

		} else if (i == UPGRAV) {
			downWardsForce = new Vector2(0, fauxGravity);
			jumpChecker.y = -4.0f; 
			downDirection = new Vector2 (0f, 1f); 
			col.offset = new Vector2(0f, 0.02f);
			col.size = new Vector2(0.12f, 0.07f);

		} else if (i == LEFTGRAV) {
			downWardsForce = new Vector2(-fauxGravity, 0);
			jumpChecker.x = 3.0f;
			downDirection = new Vector2 (-1f, 0f);
			col.offset = new Vector2(0f, 0.023f);
			col.size = new Vector2(0.06f, 0.11f);

		} else if (i == RIGHTGRAV) {
			downWardsForce = new Vector2(fauxGravity, 0);
			jumpChecker.x = -3.0f;
			downDirection = new Vector2 (1f, 0f);
			col.offset = new Vector2(0f, 0.025f);
			col.size = new Vector2(0.07f, 0.11f);

		}
	}

	private bool shouldNotJump(){

		RaycastHit2D detection = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + 1f), jumpChecker, 5f, toHit);

		//Debug.Log ("Detection is towards: " + jumpChecker.ToString());
		
		if (detection.collider != null) {
			//Debug.Log(detection.collider.gameObject.ToString());
			return true;
		} else {
			return false;
		}


	}

	private bool shouldNotTurnBackwards(){
		
		RaycastHit2D detection = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y ), downDirection, canyionDistance, toHit);
		
		//Debug.Log ("Detection is towards: " + downDirection.ToString());
		
		if (detection.collider != null) {
			//Debug.Log(detection.collider.gameObject.ToString());
			return true;
		} else {
			return false;
		}
		
		
	}


	//NOT CURRENTLY IN USE TODO: DETERMINE IF NESSISARY
	/*private bool reverseFlip(){
		RaycastHit2D detection = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + 1f), -downDirection, 2f, toHit);


		if (detection.collider != null) {
			Debug.Log(detection.collider.gameObject.ToString());
			return true;
		} else {
			return false;
		}


	}*/

	//detecting to see if there is an object right infront of the walker;
	private bool collisionAhead(){

		RaycastHit2D detection = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y + 0.3f)
		                                            , currentRotation, 3f, toHit);


		if (detection.collider != null) {
		return true;
		} else {
			return false;
		}
	}


	private bool playerKillZone(){
		
		RaycastHit2D detection = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y + 1f)
		                                            , currentRotation, 3f, toHit);
		
		
		if (detection.collider != null && detection.collider.tag == "Player") {
			deathNode.setDeath(true);
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
		Gizmos.DrawRay (this.transform.position, downDirection);
		Gizmos.DrawRay (this.transform.position, -downDirection);


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
				float FoundDistance = Vector3.Distance(this.transform.position, hitBarrier[i].position);
				
				RaycastHit2D hit = Physics2D.Raycast (this.transform.position, (hitBarrier[i].position - this.transform.position), FoundDistance, toHit);
				
				if(hit.collider != null){
					if(hit.collider.tag == "Player"){
						player = hit.collider.gameObject;
						return true;
					} 
				} 
			}
		}
		return false;
	}

}

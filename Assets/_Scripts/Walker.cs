using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public Vector3[] patrolPoints;
	private int currentPoint = 0;

	private Transform[] hitBarrier;
	public LayerMask toHit;
	
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


	private GameObject bodyControl;

	private int curRotInt;
	private int curDownInt;

	private Vector3 currentRotation;
	private Vector2 downWardsForce;





	void Awake(){
		bodyControl = transform.FindChild ("root");

		this.transform.position = patrolPoints [0];

		curRotInt = 0;
		curDownInt = 0;

		indexis = new int[4, 4, 3] {
			{{90,2 ,3}, {-90,2,1}, {0,-1,-1}, {0,-1,-1}}, 
			{{0,-1,-1}, {0,-1,-1}, {90,1 ,2}, {-90,1,0}},
			{{-90,3,3}, {90,3 ,1}, {0,-1,-1}, {0,-1,-1}},
			{{0,-1,-1}, {0,-1,-1}, {-90,0,2}, {90,0 ,0}}
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
				this.GetComponent<Rigidbody2D>().velocity = new Vector3(1.0f, 150f);
				                                                    

			}

		}


		this.GetComponent<Rigidbody2D> ().velocity += downWardsForce;
		Debug.Log ("applied downwards force: " + downWardsForce.ToString ());
		Debug.Log ("current velocity: " + this.GetComponent<Rigidbody2D> ().velocity.ToString ());

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





		//bodyControl.transform.Rotate

	}


	private void UpdateRotation(int i){

		switch (i) {
		case LEFT: currentRotation = -transform.right; break;
		case RIGHT: currentRotation = transform.right; break;
		case UP: currentRotation = transform.up; break;
		case DOWN: currentRotation = -transform.up; break;

		}
	}


	private void UpdateDownwardsForce(int i){
		
		switch (i) {
		case DOWNGRAV: downWardsForce = new Vector2(0, -fauxGravity); break;
		case UPGRAV: downWardsForce = new Vector2(0, fauxGravity); break;
		case LEFTGRAV: downWardsForce = new Vector2(-fauxGravity, 0); break;
		case RIGHTGRAV: downWardsForce = new Vector2(fauxGravity, 0); break;
			
		}
	}

	private bool shouldNotJump(){

		RaycastHit2D detection = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y + 1f), new Vector2 (currentRotation.x + 2.5f, currentRotation.y + 3f), 5f, toHit);
		
		
		if (detection.collider != null) {
			return true;
		} else {
			return false;
		}


	}

	//detecting to see if there is an object right infront of the walker;
	private bool collisionAhead(){

		RaycastHit2D detection = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y + 1f), currentRotation, 3f, toHit);


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
		Gizmos.DrawRay(new Vector2 (this.transform.position.x, this.transform.position.y + 1f), new Vector2 (currentRotation.x + 2f, currentRotation.y + 3f));


		for (int i = 1; i < patrolPoints.Length; i++) {
			Gizmos.DrawLine(patrolPoints[i-1],patrolPoints[i]);
		}


		if (loop) {
			Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1], patrolPoints[0]);
		}

	}

}

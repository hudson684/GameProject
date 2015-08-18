using UnityEngine;
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
	public LayerMask toHit;
	public int maxGrappleDisance = 10;
	private GameObject cursor;
	public GameObject grapple;
	public GameObject cable;
	public float grappleSpeed = 3;
	public int maxAddedRope = 3;

	public float inverseRopeSize = 3f;

	public Transform firePos;

	public AudioClip audShoot;
	public AudioClip audHit;
	public AudioClip audError;
	AudioSource playerAudio;

	private string objName = " ";



	private GameObject playerGameObj;
	private PlayerControl player;
	private Vector3 mousePosition;
	private int ropeLength = 0;
	private GameObject[] ropeArr;

	private bool madeLast = false;
	private bool grappling = false;

	private bool shooting = false;
	private bool retracting = false;
	private bool playerKilled = false;



	Vector3 firePointPosition;

	private int oldLayer = -1;
	private int playerLayer;

	void Start(){
		cursor = GameObject.FindGameObjectWithTag ("Cursor");
		player = (PlayerControl) this.GetComponentInParent(typeof(PlayerControl));
		playerGameObj = this.transform.parent.gameObject;
		playerAudio = GetComponent<AudioSource>();
		ropeArr = new GameObject[(maxGrappleDisance *( (int)inverseRopeSize)+2)]; //make rope array expected size times two to accoutn for the size of the rope object;
	
		//layers that the grapple cannot hit
		playerLayer = 1 << LayerMask.NameToLayer("Player");
	
	}


	void DisableCollider(Collider col)
	{
		oldLayer = col.gameObject.layer;
		col.gameObject.layer = playerLayer;
	}
	
	void EnableCollider(Collider col)
	{
		col.gameObject.layer = oldLayer;
	}


	// Update is called once per frame
	void Update () {

		firePointPosition = new Vector3 (transform.position.x, transform.position.y, 0);
		mousePosition = cursor.transform.position;


		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f){

			if(!grappling){
				if (!madeLast && !shooting) {

					shooting = true;
					StartCoroutine("Shoot");
					grappling = false;					
			
				} else if ((madeLast && !retracting)){


					retracting = true;
					grappling = true;
					StartCoroutine("retract");
					
					grappling = false;
					player.setGrappling(false);
				}

			}
		}


		if (playerKilled) {
			if(madeLast && !retracting){
				retracting = true;
				grappling = true;
				StartCoroutine("retract");
				
				grappling = false;
				player.setGrappling(false);

			}


		}
		
	}
	//shoot the grapple in the direction of the mouse
	IEnumerator Shoot(){	


		Vector3 currentCursor = mousePosition;

		grappling = true;
		player.setGrappling (true);

		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, (mousePosition - firePointPosition), maxGrappleDisance, toHit);

		if (hit.collider != null) {
		
			Vector3 hitPosition = hit.point;
			float flnoLoops = (hit.distance * inverseRopeSize)/3f;
			
			if(flnoLoops > 0){

				Quaternion zeroRotation = new Quaternion (0, 0, 0, 0);
				GameObject head = Instantiate (grapple, firePointPosition, zeroRotation) as GameObject;
				//Debug.Log("making head at tranform position: " + transform.position.ToString());

				playerAudio.PlayOneShot(audShoot);

				ropeArr[0] = head;
				ropeLength = 1;
				
				//while the grapple is not at the target position, incriment the movement;
				float t = 0.0f;
				int i = 1;


				while (t != 1f) {

					playerGameObj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
					playerGameObj.GetComponent<Rigidbody2D>().angularVelocity = 0f;

					GameObject previous = ropeArr[i - 1];
					GameObject rope = Instantiate (cable, this.transform.position, zeroRotation) as GameObject;

					//Debug.Log("made rope object at: " + head.transform.position.ToString());

					rope.name = "rope:" + i;

					rope.GetComponent<HingeJoint2D> ().enabled = true;
					rope.GetComponent<BoxCollider2D> ().enabled = true;
					rope.GetComponent<HingeJoint2D> ().connectedBody =  previous.GetComponent<Rigidbody2D> ();
					rope.GetComponent<Rigidbody2D>().gravityScale = 1;
					ropeArr[i] = rope;
					i++;


					previous = ropeArr[i - 1];
					GameObject ropeTwo = Instantiate (cable, this.transform.position, zeroRotation) as GameObject;
					
					//Debug.Log("made rope object at: " + head.transform.position.ToString());
					
					ropeTwo.name = "rope:" + i;
					
					ropeTwo.GetComponent<HingeJoint2D> ().enabled = true;
					ropeTwo.GetComponent<BoxCollider2D> ().enabled = true;
					ropeTwo.GetComponent<HingeJoint2D> ().connectedBody =  previous.GetComponent<Rigidbody2D> ();
					ropeTwo.GetComponent<Rigidbody2D>().gravityScale = 1;
					ropeArr[i] = ropeTwo;
					i++;


					previous = ropeArr[i - 1];
					GameObject ropeThree = Instantiate (cable, this.transform.position, zeroRotation) as GameObject;
					
					//Debug.Log("made rope object at: " + head.transform.position.ToString());
					
					ropeThree.name = "rope:" + i;
					
					ropeThree.GetComponent<HingeJoint2D> ().enabled = true;
					ropeThree.GetComponent<BoxCollider2D> ().enabled = true;
					ropeThree.GetComponent<HingeJoint2D> ().connectedBody =  previous.GetComponent<Rigidbody2D> ();
					ropeThree.GetComponent<Rigidbody2D>().gravityScale = 1;
					ropeArr[i] = ropeThree;
					
					
					GameObject grappleEnd = ropeArr [i];
					this.GetComponent<HingeJoint2D> ().enabled = true;
					this.GetComponent<HingeJoint2D> ().connectedBody = grappleEnd.GetComponent<Rigidbody2D> ();

					i++;
					ropeLength = i;
					//t += (1f / (flnoLoops - 2f));
					t += (1f / (flnoLoops));
					if(t > 1f){
						t = 1f;
					}
					head.transform.position = Vector3.Lerp (firePointPosition, hitPosition, t);


					
					for(int j = 1; j < i; j++){
						//Debug.Log(j);
						float p = 1f - ((float)j*(1f/(float)i));
						//Debug.Log(ropeArr[j].ToString() +" "+ p);
						
						ropeArr[j].transform.position = Vector3.Lerp (firePointPosition, ropeArr[0].transform.position, p);	
						//Debug.Log("the grapple hook is at: " + ropeArr[0].transform.position.ToString());
						//Debug.Log("rope " +j+ " should be at: " + (Vector3.Lerp (firePointPosition, ropeArr[0].transform.position, p).ToString()));
						//Debug.Log("rope " +j+ " is at: " + ropeArr[j].transform.position.ToString());
					}


					yield return null;
				}
				playerAudio.PlayOneShot(audHit);
				



				//now that it as at the destination activate and apply the grapple head to the hit position
				HingeJoint2D grappleHinge = ropeArr[0].GetComponent<HingeJoint2D> ();
				grappleHinge.enabled = true;

				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Object")){
					grappleHinge.connectedBody = hit.collider.attachedRigidbody;
					objName = hit.collider.name;
				} else {
					grappleHinge.connectedAnchor = hit.point;
				}
				madeLast = true;
			}

			shooting = false;
		} else {
			playerAudio.PlayOneShot(audError);

			grappling = false;
			player.setGrappling(false);
			shooting = false;
		}

		
	}



	IEnumerator retract(){

		//while the grapple is not at the target position, incriment the movement;
		float t = 0.0f;
		int i = 1;
		Vector3 startPosition = ropeArr [0].transform.position;
		float totalLength = (ropeLength);

		ropeArr [0].GetComponent<HingeJoint2D> ().connectedBody = null;

		objName = " ";

		while (i <= totalLength) {

			for(int j = 0; j < 10; j++){
				if(i < totalLength) {
					GameObject toDelete = ropeArr[i];
					ropeArr[0].transform.position = ropeArr[i].transform.position;
					Destroy(toDelete);
					i++;
					ropeLength--;
				}
			}

			if(i == totalLength){
				Destroy(ropeArr[0]);
				i++;
			}
			yield return null;
		}

		madeLast = false;
		retracting = false;
	}


	public void reduce(){
		if (ropeLength > 2) {
			GameObject grappleEnd = ropeArr [ropeLength - 1];
			Destroy(grappleEnd);
			ropeLength--;
			grappleEnd = ropeArr [ropeLength - 1];
			this.GetComponent<HingeJoint2D> ().connectedBody = grappleEnd.GetComponent<Rigidbody2D> ();
		}

	}

	public void add(){
		if (ropeLength < ropeArr.Length) {

			GameObject grappleEnd = ropeArr [ropeLength - 1];
			Vector3 AdjustedPosition = new Vector3(this.transform.position.x,this.transform.position.y, 0.0f);

			GameObject newRope = Instantiate (cable, AdjustedPosition, grappleEnd.transform.rotation) as GameObject;

			newRope.GetComponent<HingeJoint2D> ().enabled = true;
			this.GetComponent<HingeJoint2D> ().enabled = true;
			newRope.GetComponent<HingeJoint2D> ().connectedBody =  grappleEnd.GetComponent<Rigidbody2D> ();
			newRope.GetComponent<BoxCollider2D>().enabled = true;
			this.GetComponent<HingeJoint2D> ().connectedBody = newRope.GetComponent<Rigidbody2D> ();

			ropeArr[ropeLength] = newRope;
			ropeLength++;
		}

	}

	public void retractGrapple()
	{
		StartCoroutine ("retract");
	}


	public string getObjName(){
		return objName;
	}

	public void setPlayerKilled(bool value){

		playerKilled = value;

	}

}



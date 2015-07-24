using UnityEngine;
using System.Collections;

public class Partol : MonoBehaviour {



	public Vector3 pointB;
	public Vector3 pointA;

	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	public GameObject bullet;
	
	private bool triggered = false;
	private bool patroling = true;

	public Material alert;
	public Material normal;

	private DynamicLight light2D;

	public float waitTime = 1.5f;


	public float Speed = 10f;



	//set up the patrolers 2d light asset
	IEnumerator Start()
	{
		light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
		light2D.setMainMaterial(normal);

		//finds all the childern of the patroler (for use in player detection);
		hitBarrier = transform.GetComponentsInChildren<Transform>();


		//while the patroler is alive, lerp between two points
		while (true) {
			yield return StartCoroutine(MoveObject(transform, pointA, pointB));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA));
		}

	}

	//if player is found, and hasn't already been triggered, stop the patroling and wait a few seconds, then shoot at player
	void Update(){
		if (foundPlayer() && !triggered) {
			light2D.setMainMaterial(alert);
			patroling = false;
			StartCoroutine("waitSeconds");
		}
	}


	//delay for shooting at player
	IEnumerator waitSeconds(){
		triggered = true;
		yield return new WaitForSeconds(waitTime);
		shootAtPlayer();
	}


	//lerps the patroler between two positions at a certain rate
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos)
	{
			

		Vector3 vecRotation = new Vector3(0.0f, 180f, 0.0f);

		Vector3 lightRotation = new Vector3 (0.0f, 0.0f, 180f);

		light2D.gameObject.transform.Rotate(lightRotation);
		this.transform.Rotate(vecRotation);
		float i = 0.0f;
		float adjustSpeed = Speed / 30f;
		while (i < 1.0f) {
			if (patroling) {
				i += Time.deltaTime * adjustSpeed;
				thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			}
		yield return null; 
		}
	}

	//if the player is still in its line of sight after the wait time, then shoot the player and go back to normal
	//if not, just go back to normal.
	void shootAtPlayer(){

		if (foundPlayer ()) {
			StartCoroutine ("seekDestroy");
			patroling = true;
			light2D.setMainMaterial(normal);

		} else {

			triggered = false;
			patroling = true;
			light2D.setMainMaterial(normal);
		}
	}
	
	
	//this is the code for the bullet that is being sent to the player, it is defigned so that it will never miss
	IEnumerator seekDestroy(){
		GameObject instaKill = Instantiate(bullet,this.transform.position, this.transform.rotation) as GameObject;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		float t = 0.0f;
		while (t < 1){
			t += 0.1f;
			instaKill.transform.position = Vector3.Lerp(this.transform.position,player.transform.position, t);
			yield return null;
		}

		transform.position = pointA;
		triggered = false;
		
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


	//CODE BY LIAM, TO MAKE DEVELOPMENT EASIER
	void OnDrawGizmos(){
		Gizmos.DrawLine(pointA,pointB);
	}

}

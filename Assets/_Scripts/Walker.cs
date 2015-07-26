using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public Vector3[] patrolPoints;

	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	private bool triggered = false;
	private bool patroling = true;

	public Material alert;
	public Material normal;

	private DynamicLight light2D;

	public float waitTime = 1.5f;
	public float Speed = 10f;



	//set up the patrolers 2d light asset
	void Start()
	{
		light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
		light2D.setMainMaterial(normal);

		//finds all the childern of the patroler (for use in player detection);
		hitBarrier = transform.GetComponentsInChildren<Transform>();

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

		for (int i = 1; i < patrolPoints.Length; i++) {
			Gizmos.DrawLine(patrolPoints[i-1],patrolPoints[i]);
		}

	}

}

using UnityEngine;
using System.Collections;

public class Partol2 : MonoBehaviour {



	public Vector3 pointB;
	public Vector3 pointA;

	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	public GameObject bullet;
	
	private bool triggered = false;
	private bool patroling = true;

	public Material m;

	private DynamicLight light2D;

	public float waitTime = 1.5f;

	
	IEnumerator Start()
	{
		light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
		hitBarrier = transform.GetComponentsInChildren<Transform>();


		while (true) {
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 5.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 5.0f));
		}

	}


	void Update(){
		if (foundPlayer() && !triggered) {
			light2D.setMainMaterial(m);
			patroling = false;
			Debug.Log("should work");
			StartCoroutine("waitSeconds");
		}
	}

	IEnumerator waitSeconds(){
		triggered = true;
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("shootingPlayer");
		shootAtPlayer();
	}
	
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
			

		Vector3 vecRotation = new Vector3(0.0f, 180f, 0.0f);

		Vector3 lightRotation = new Vector3 (0.0f, 0.0f, 180f);

		light2D.gameObject.transform.Rotate(lightRotation);
		this.transform.Rotate(vecRotation);
		var i = 0.0f;
		var rate = 1.0f / time;
		while (i < 1.0f) {
			if (patroling) {
				i += Time.deltaTime * rate;
				thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			}
		yield return null; 
		}
	}


	void shootAtPlayer(){

		Debug.Log ("found player: " + foundPlayer ().ToString ());
		if (foundPlayer ()) {
			Debug.Log ("shooting at player");
			StartCoroutine ("seekDestroy");
			patroling = true;
			Debug.Log("patroling: " + patroling.ToString ());
		} else {
			Debug.Log("could not find player");
			triggered = false;
			patroling = true;
			Debug.Log("patroling: " + patroling.ToString ());
		}
	}
	
	
	
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
	
	bool foundPlayer(){
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

	void OnDrawGizmos(){
		Gizmos.DrawLine(pointA,pointB);
	}

}

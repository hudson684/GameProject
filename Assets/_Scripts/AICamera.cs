 using UnityEngine;
using System.Collections;

public class AICamera : MonoBehaviour {

	public LayerMask toHit;

	public Transform camHead;

	public GameObject origin;
	public GameObject target;

	public Material alert;

	public DynamicLight light2D;

	public float waitTime = 0.1f;

	private GameObject player;


	public float maxSwing = -90f;
	public float minSwing =  90f;
	private float[] swingPoints;
	private int swingPoint = 0;
	private Vector3 rotation;

	public GameObject[] nodes;

	public float swingSpeed = 2f;
	

	private bool triggered = false;
	private bool active = true;




	// Use this for initialization
	void Start () {
		swingPoints = new float[2] {maxSwing, minSwing};

		light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
	}
	
	
	// Update is called once per frame
	void Update () {

		activation();

		if (active) {

			rotateCamrea();

			if (foundPlayer () && !triggered) {

				light2D.setMainMaterial(alert);
				StartCoroutine ("waitSeconds");
			}
		} 
	}


	void rotateCamrea(){

		if (swingPoint < 2) {

			//Debug.Log("should be rotating");
			float nextPoint = swingPoints [swingPoint];
			float rotationDirection = nextPoint - camHead.transform.rotation.eulerAngles.z;

			if (camHead.transform.rotation.eulerAngles.z >= nextPoint - 5f && camHead.transform.rotation.eulerAngles.z <= nextPoint + 5f) {
				//Debug.Log("dont need to do anything");
				swingPoint++;
			} else {
				camHead.Rotate(new Vector3(0f, 0f,((rotationDirection) / 100f) * swingSpeed)); 

			} 
		} else {
			swingPoint = 0;
		}



	}

	
	IEnumerator waitSeconds(){
		triggered = true;
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("shootingPlayer");
		shootAtPlayer();
	}
	
	
	void shootAtPlayer(){
		if (foundPlayer ()) {
			triggered = true;
			Debug.Log ("shooting at player");


			target.SetActive(true);
			target.transform.position = player.transform.position;

			origin.SetActive(true);


		} else {
			Debug.Log("missed");
			triggered = false;

		}
	}

	

	
	bool foundPlayer(){

		Debug.Log ("trying to find the player");
		for(int i = 0; i < (nodes.Length -1); i++){
			float distance = Vector3.Distance(camHead.transform.position, nodes[i].transform.position);
			
			RaycastHit2D hit = Physics2D.Raycast (camHead.transform.position, (nodes[i].transform.position - camHead.transform.position), distance, toHit);
			
			if(hit.collider != null){
				if(hit.collider.tag == "Player"){
					Debug.Log("found player");
					player = hit.collider.gameObject;
					return true;
				} 
			} 
			
		}
		return false;
	}


	void activation(){

		if (active) {
			light2D.gameObject.SetActive(true);
		} else {
			light2D.gameObject.SetActive(false);
		}
	}


	public void isActive(bool setActive){
		active = setActive;
	}
}

using UnityEngine;
using System.Collections;

public class AICamera : MonoBehaviour {
	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	public bool changeColour = true;

	public GameObject lightBox;
	private Light cameraLight;

	public GameObject bullet;

	public Material alert;

	private DynamicLight light2D;

	public float waitTime = 0.1f;


	public float maxSwing = -90f;
	public float minSwing =  90f;
	private float[] swingPoints;
	private int swingPoint = 0;
	private Vector3 rotation;


	private Color green = Color.green;
	private Color red = Color.red;

	private bool triggered = false;
	private bool active = true;


	// Use this for initialization
	void Start () {
		swingPoints = new float[2] {maxSwing, minSwing};

		light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
		hitBarrier = transform.GetComponentsInChildren<Transform>();

		cameraLight = lightBox.GetComponent<Light>();
		//spotLight = spot.GetComponent<Light>();
	}
	
	
	// Update is called once per frame
	void Update () {

		activation();

		if (active) {

			if (foundPlayer () && !triggered) {

				Debug.Log ("should change colour now");
				if (changeColour) {
					light2D.setMainMaterial(alert);
				}
				//shootAtPlayer();
				StartCoroutine ("waitSeconds");
			}
		} 
	}


	void rotateCamrea(){

		if (swingPoint < 2) {
			float nextPoint = swingPoints [swingPoint];
			float rotationDirection = nextPoint - this.transform.rotation.z;
			
			rotation = this.transform.rotation.eulerAngles;
			
			if (rotation.z - nextPoint < 5) {
				swingPoint++;
			} else {
				rotation.z += rotation.z - nextPoint / 10f; 
			} 
		} else {
			swingPoint = 0;
		}



	}

	
	IEnumerator waitSeconds(){
		triggered = true;
		//yield return null;
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("shootingPlayer");
		shootAtPlayer();
	}
	
	
	void shootAtPlayer(){
		if (foundPlayer ()) {
			triggered = true;
			Debug.Log ("shooting at player");
			StartCoroutine ("seekDestroy");	

			if (changeColour) {
				cameraLight.color = green;
			}
		} else {
			Debug.Log("missed");
			triggered = false;
			if (changeColour) {
				cameraLight.color = green;
			}
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
		triggered = false;

	}

	bool foundPlayer(){
		for(int i = 0; i < (hitBarrier.Length -1); i++){
			if(hitBarrier[i].tag == "CameraNodes"){
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


	void activation(){

		if (active) {
			if (changeColour) {
				cameraLight.color = green;

				light2D.gameObject.SetActive(true);
			}
		} else {
			if (changeColour) {
				cameraLight.color = Color.black;
				light2D.gameObject.SetActive(false);
			}

		}
	}


	public void isActive(bool setActive){
		active = setActive;
	}
}

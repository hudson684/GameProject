 using UnityEngine;
using System.Collections;

public class AICamera : MonoBehaviour {
	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	public bool changeColour = true;

	public GameObject lightBox;
	private Light cameraLight;
	public Transform camHead;

	public GameObject bullet;

	public Material alert;

	private DynamicLight light2D;

	public float waitTime = 0.1f;


	public float maxSwing = -90f;
	public float minSwing =  90f;
	private float[] swingPoints;
	private int swingPoint = 0;
	private Vector3 rotation;

	public float swingSpeed = 2f;


	private Color green = Color.green;
	private Color red = Color.red;

	private bool triggered = false;
	private bool active = true;




	// Use this for initialization
	void Start () {
		swingPoints = new float[2] {maxSwing, minSwing};

		//light2D = (DynamicLight) this.GetComponentInChildren(typeof(DynamicLight));
		hitBarrier = transform.GetComponentsInChildren<Transform>();

		//cameraLight = lightBox.GetComponent<Light>();
		//spotLight = spot.GetComponent<Light>();
	}
	
	
	// Update is called once per frame
	void Update () {

		activation();

		if (active) {

			rotateCamrea();

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

			//Debug.Log("should be rotating");
			float nextPoint = swingPoints [swingPoint];
			float rotationDirection = nextPoint - camHead.transform.rotation.eulerAngles.x;


			Debug.Log("current x: " + camHead.transform.rotation.eulerAngles.x.ToString());
			Debug.Log("next point leway min: " + (rotationDirection - 5f).ToString());
			Debug.Log("next point leway max: " + (rotationDirection + 5f).ToString());

			//rotation = camHead.transform.rotation.eulerAngles;
			
			if (camHead.transform.rotation.eulerAngles.x >= nextPoint - 5f && camHead.transform.rotation.eulerAngles.x <= nextPoint + 5f) {
				Debug.Log("dont need to do anything");
				swingPoint++;
			} else {
				camHead.Rotate(new Vector3(((rotationDirection) / 100f) * swingSpeed, 0f, 0f)); 

				//Debug.Log("rotation of x: " + rotation.x.ToString());
				Debug.Log("next point dev 10: " + (((rotationDirection) / 100f) * swingSpeed).ToString());

				//Debug.Log((0f + -9).ToString());
			} 
		} else {
			swingPoint = 0;
		}



		//camHead.Rotate (rotation.x, 0f, 0f);

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
				//cameraLight.color = green;

				//light2D.gameObject.SetActive(true);
			}
		} else {
			if (changeColour) {
				//cameraLight.color = Color.black;
				//light2D.gameObject.SetActive(false);
			}

		}
	}


	public void isActive(bool setActive){
		active = setActive;
	}
}

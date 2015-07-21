using UnityEngine;
using System.Collections;

public class parallaxing : MonoBehaviour {

	public Transform[] backgrounds; //array of all the back and foregrounds to be parallaxed
	private float[] parallaxScales; //the proportion of the cameras movement to moev the backgrounds bye
	public float smoothing = 1f;    //how smooth the parallax is going to be, make sure to set this above 0

	private Transform cam;               //reference to the main cameras transform
	private Vector3 previousCamPosition; //store the position of the camera in the previous frame

	//called befor start().
	void Awake () {
		//setup cam reference
		cam = Camera.main.transform;

	}

	// Use this for initialization
	void Start () {
		//store previous frame at the current frames cam position
		previousCamPosition = cam.position;

		//assigning corresponding parallax scales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}

	
	}
	
	// Update is called once per frame
	void Update () {

		//for each background
		for (int i = 0; i < backgrounds.Length; i++) {
			//the paralax is the oposite of the camera movement bc the previous 
			//frame multiplied by the scale
			float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

			//set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//create a target pos which is the backgrounds current pos with its target x pos
			Vector3 backgroundTargetPosition = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			//fade between current pos and the target pos using lerp;
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);

		}

		//set the previous cam pos to the cam pos at the end of the frame
		previousCamPosition = cam.position;
	
	}
}

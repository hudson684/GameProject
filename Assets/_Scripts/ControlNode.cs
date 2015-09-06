using System;
using UnityEngine;

public class ControlNode : MonoBehaviour
{
	bool paused = false;
	Canvas pauseCanvas;

	private static bool canGrapple = false;
	
	private static bool canZeroGravMove = true;

	private Vector3 playerPosition;
	private float grappleDistance;

	private GameObject CheckPointMarker;



	void Start(){
		pauseCanvas = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>();
		CheckPointMarker = GameObject.FindGameObjectWithTag ("CheckPointMarker");
	
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = togglePause ();
		}
	}



	public void setCanGrapple(bool value){
		canGrapple = value;
	} 

	public bool getCanGrapple(){
		return canGrapple;
	}

	
	public void setZeroGrav(bool value){
		canZeroGravMove = value;
	} 
	
	public bool getZeroGrav(){
		return canZeroGravMove;
	}

	public void setPlayerPosition(Vector3 value){
		playerPosition = value;
	} 
	
	public Vector3 getPlayerPosition(){
		return playerPosition;
	}

	
	public void setGrappleDistance(float value){
		grappleDistance = value;
	} 
	
	public float getGrappleDistance(){
		return grappleDistance;
	}
	

	







	public void save(){

		Debug.Log ("saved game");

		PlayerPrefs.SetString("SaveLevelKey", Application.loadedLevelName);

		float x = CheckPointMarker.transform.position.x;
		float y = CheckPointMarker.transform.position.y;
		float z = CheckPointMarker.transform.position.z;

		PlayerPrefs.SetFloat ("checkX", x);
		PlayerPrefs.SetFloat ("checkY", y);
		PlayerPrefs.SetFloat ("checkZ", z);

		Debug.Log ("moved the checkpoint save to: " + new Vector3 (x, y, z).ToString ());
	}

	public void load(){
		Application.LoadLevel(PlayerPrefs.GetString("SaveLevelKey"));

		float x = PlayerPrefs.GetFloat ("checkX");
		float y = PlayerPrefs.GetFloat ("checkY");
		float z = PlayerPrefs.GetFloat ("checkZ");

		CheckPointMarker.transform.position = new Vector3 (x, y, z);



		paused = togglePause();
	}

	public void restart(){
		Application.LoadLevel(Application.loadedLevel);
		unpause ();
	}

	public void quit(){
		Application.LoadLevel(0);
	}

	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			pauseCanvas.enabled = false;
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			pauseCanvas.enabled = true;
			Time.timeScale = 0f;
			return(true);    
		}
	}

	public void unpause(){
		paused = togglePause();
	}
}
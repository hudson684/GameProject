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
	private bool isPlayerLeft = false;

	private GameObject CheckPointMarker;

	private int levelIndex;



	void Start(){
		pauseCanvas = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>();
		CheckPointMarker = GameObject.FindGameObjectWithTag ("CheckPointMarker");
		levelIndex = Application.loadedLevel;
	}
	
	void Update()
	{
		gameControl ();

		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = togglePause ();
		}
	}


	private void gameControl(){

		if (Input.GetKeyDown (KeyCode.F1)) {
			Application.LoadLevel (levelIndex - 1);
		}

		if(Input.GetKeyDown(KeyCode.F2)){
			Application.LoadLevel(levelIndex + 1);
		}

		if (Input.GetKeyDown(KeyCode.F3)){
			Debug.Log("god mode activated");
			this.GetComponent<DeathNode>().setGodMode(true);
		}

		if (Input.GetKeyDown(KeyCode.F4)){
			Debug.Log("god mode deactivated");
			this.GetComponent<DeathNode>().setGodMode(false);
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

	public bool getPlayerFacingLeft(){
		return isPlayerLeft;
	}

	public void setPlayerFacingLeft(bool isLeft){
		isPlayerLeft = isLeft;	
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
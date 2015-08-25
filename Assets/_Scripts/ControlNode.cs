using System;
using UnityEngine;

public class ControlNode : MonoBehaviour
{
	bool paused = false;
	Canvas pauseCanvas;

	private static bool canGrapple = true;
	
	private static bool canZeroGravMove = true;

	private Vector3 playerPosition;
	private float grappleDistance;




	void Start(){
		pauseCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
		
	
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
		PlayerPrefs.SetString("SaveLevelKey", Application.loadedLevelName);
	}

	public void load(){
		Application.LoadLevel(PlayerPrefs.GetString("SaveLevelKey"));
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
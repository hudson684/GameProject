﻿using System;
using UnityEngine;

public class ControlNode : MonoBehaviour
{
	bool paused = false;
	Canvas pauseCanvas;

	void Start(){
		pauseCanvas = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = togglePause ();
		}
	}

	/*
	void OnGUI()
	{
		if(paused)
		{
			GUILayout.Label("Game is paused!");
			if(GUILayout.Button("Unpause")){
				paused = togglePause();
			}


			if(GUILayout.Button("Save")){
				save();
			}


			if(GUILayout.Button("Load")){
				load();
			}

			if(GUILayout.Button("Exit")){
				Application.Quit();
			}

		}
	}

	*/

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
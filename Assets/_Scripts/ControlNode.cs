using System;
using UnityEngine;

public class ControlNode : MonoBehaviour
{
	bool paused = false;
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = togglePause ();
		}
	}
	
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

	void save(){
		PlayerPrefs.SetString("SaveLevelKey", Application.loadedLevelName);
	}

	void load(){
		Application.LoadLevel(PlayerPrefs.GetString("SaveLevelKey"));
		paused = togglePause();
	}
	
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}
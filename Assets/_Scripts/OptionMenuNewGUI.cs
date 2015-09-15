﻿using UnityEngine;
using System.Collections;

public class OptionMenuNewGUI : MonoBehaviour {

	private bool min = false;
	private bool low = false;
	private bool normal = true;
	private bool high = false;

	private bool titleMenu = false;
	private Canvas canvas;


	// Use this for initialization
	void Start () {
		GameObject pause = GameObject.FindGameObjectWithTag ("PauseMenu");

		canvas = GameObject.FindGameObjectWithTag("OptionsMenu").GetComponent<Canvas>();

		if (pause == null) {
			Debug.Log("In title menu");
			titleMenu = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void open(){
		Debug.Log ("pressed open");
		canvas.enabled = true;

	}

	public void close(){
		canvas.enabled = false;
	}


	public void volChange(float volume){

		AudioListener.volume = volume;
	}

	public void setBoolLow(){
		low = true;
		min = false;
		high = false;
		normal = false;

	}

	public void setBoolMin(){
		low = false;
		min = true;
		high = false;
		normal = false;

	}

	public void setBoolNormal(){
		low = false;
		min = false;
		high = false;
		normal = true;

		
	}
	
	public void setBoolHigh(){
		low = false;
		min = false;
		high = true;
		normal = false;
		
	}

	public void settingChange(){

		//Debug.Log("Min: " + min.ToString() + "Low: " + low.ToString() + "Normal: " + normal.ToString() + "High: " + high.ToString());

		if (min) {
			QualitySettings.SetQualityLevel(0, true);

		} else if (low) {
			QualitySettings.SetQualityLevel(1, true);

		} else if (normal) {
			QualitySettings.SetQualityLevel(3, true);

		} else if (high){
			QualitySettings.SetQualityLevel(5, true);
		}

	}

}

using UnityEngine;
using System.Collections;

public class TimerLaser : Laser {

	//Controls the Toggle duration for the timer Laser
	public float startTime;
	public float onDuration;
	public float offDuration;
	public float telegraphTime = 1f;
	private float onTimer;
	private float offTimer;
	private bool laserOn = true;

	// Use this for initialization
	void Start () {
		setupLaser();
	}
	
	// Update is called once per frame
	void Update () {
		traceLaser();
		timerToggle();
	}

	/// <summary>
	/// Toggles the laser On and off according to a timer
	/// </summary>
	void timerToggle(){
		if(laserOn){
			if(onTimer > 0){
				//countdown timer
				onTimer -= Time.deltaTime;
			}else{
				//toggle laserOff
				toggleOff();
				//Reset on Timer
				onTimer = onDuration;
				laserOn = false;
			}
		}else{
			if(offTimer > 0){
				//countdown timer
				offTimer -= Time.deltaTime;
				if(offTimer < telegraphTime){
					traceFlicker();
				}
			}else{
				//toggle laserOff
				kill = true;
				toggleOn();
				//Reset on Timer
				offTimer = offDuration;
				laserOn = true;
			}
		}
	}//end timer Toggle
}

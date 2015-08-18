using UnityEngine;
using System.Collections;

public class Sweep : MonoBehaviour {

	//Sweep variables
	[Range(0,360)]public float angleA;
	[Range(0,360)]public float angleB;
	[Range(0.01f,5f)]public float swingSpeed;

	private Quaternion upAngle, downAngle;
	private bool up;

	//timer Variables

	public float delay;
	private float adjust;

	// Use this for initialization
	void Start () { 
		float maxAngle = angleA > angleB ? angleA : angleB;
		float minAngle = angleA > angleB ? angleB : angleA;

		//debugging
		//string foo =  angleA > angleB ? "Angle a is bigger": "Angle B is bigger";
		//Debug.Log(foo);

		upAngle = Quaternion.Euler(0,0,maxAngle);
		downAngle = Quaternion.Euler(0,0,minAngle);
		transform.rotation = upAngle;
		adjust = delay;
	}
	
	// Update is called once per frame
	void Update () {
		swivel();
		checkTime();
	}
	/// <summary>
	/// Rotates the transoform between 2 angles, based on a bool.
	/// </summary>
	void swivel(){
		if(up){
			transform.rotation = Quaternion.Slerp(transform.rotation, upAngle, Time.deltaTime * swingSpeed);
		}else{
			transform.rotation = Quaternion.Slerp(transform.rotation, downAngle, Time.deltaTime * swingSpeed);
		}
	}

	/// <summary>
	/// Checks the time.
	/// if adjust reaches 0, changes the direction
	/// </summary>
	void checkTime(){
		if(adjust > 0){
			adjust -= Time.deltaTime;
		}else{
			up = !up;
			adjust = delay;
		}
	}	
}

using UnityEngine;
using System.Collections;

public class MoveStars : MonoBehaviour {

	public float speed = 0.005f;
	public float countdownVal = 20f;
	private float countdown;

	private bool updown = true;
	private bool leftright = true;
	private bool toggle = true;

	// Use this for initialization
	void Start () {
		countdown = countdownVal;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (updown && leftright) {
			this.transform.Translate (new Vector3 (Random.Range (0f, speed), Random.Range (0f, speed), 0f));


		} else if (updown && !leftright){
			this.transform.Translate(new Vector3(Random.Range(-speed, 0f), Random.Range(0f, speed), 0f));
				
				
		} else if (!updown && leftright){
			this.transform.Translate(new Vector3(Random.Range(0f, speed), Random.Range(-speed, 0f), 0f));
			
			
		} else if (!updown && !leftright){
			this.transform.Translate(new Vector3(Random.Range(-speed, 0f), Random.Range(-speed, 0f), 0f));
			
			
		}
			

		if (countdown > 0) {
			countdown -= 0.05f;
		} else {
			countdown = countdownVal;
			if(toggle){
				toggle = false;
				updown = !updown;

			} else {
				toggle = true;
				leftright = !leftright;

			}

		}




	
	}
}

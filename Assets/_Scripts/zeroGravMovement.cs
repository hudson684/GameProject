using UnityEngine;
using System.Collections;

public class zeroGravMovement : MonoBehaviour {

	public float Speed = 0f;
	private float movex = 0f;
	private float movey = 0f;
	private float currentSpeedX = 0f;
	private float currentSpeedY = 0f;
	public float maxSpeed = 1f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//movex = Input.GetAxis ("Horizontal") * 0.5f;
		//movey = Input.GetAxis ("Vertical") * 0.5f;

		if (Input.GetKey (KeyCode.A)) {
			movex = -0.05f;
		} else if (Input.GetKey (KeyCode.D)) {
			movex = 0.05f;
		} else {
			movex = 0;
		}

		currentSpeedX = currentSpeedX + movex;
		currentSpeedY = currentSpeedY + movey;

		if (currentSpeedX > maxSpeed) {
			currentSpeedX = maxSpeed;
		} else if (currentSpeedX < -maxSpeed) {
			currentSpeedX = -maxSpeed;
		}

		if (currentSpeedY > maxSpeed) {
			currentSpeedY = maxSpeed;
		} else if (currentSpeedY < -maxSpeed) {
			currentSpeedY = -maxSpeed;
		}

		Debug.Log (currentSpeedX.ToString("F4"));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (currentSpeedX * Speed, currentSpeedY * Speed);
	}
}




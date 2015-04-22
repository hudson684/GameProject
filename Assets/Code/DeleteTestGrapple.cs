using UnityEngine;
using System.Collections;

public class DeleteTestGrapple : MonoBehaviour {


	Vector3 positionA;
	Vector3 positionB;
	bool var = true;
	// Use this for initialization
	void Start () {
		positionA = new Vector3(3.0f, 2.0f, 0f);
		positionB = new Vector3(5.0f, 1.0f, 0f);
		transform.Translate (positionA);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")){
			Shoot();
		}
	}

	void Shoot(){
		if (var) {
			transform.TransformDirection(positionB);
			var = !var;
		} else {
			transform.TransformDirection(positionA);
			var = !var;
		}

	}
	
}

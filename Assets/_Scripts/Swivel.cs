using UnityEngine;
using System.Collections;

public class Swivel : MonoBehaviour {

	public bool down;
	public float minAngle;
	public float maxAngle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(down){
			float nextAngle = Mathf.Lerp(transform.rotation.z, minAngle, Time.deltaTime);
			transform.Rotate(0,0,nextAngle);
		}
	}
}

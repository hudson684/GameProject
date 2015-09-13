using UnityEngine;
using System.Collections;

public class Jupiter : MonoBehaviour {

	public float rotFloat = 0.002f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		this.transform.Rotate(new Vector3(0f, 0f, rotFloat));
	
	}
}

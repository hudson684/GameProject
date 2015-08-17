using UnityEngine;
using System.Collections;

public class sweeperLaser : Laser {

	public Transform target;

	// Use this for initialization
	void Start () {
		setupLaser();
	}
	
	// Update is called once per frame
	void Update () {
		direction = target.position - transform.position;
		traceLaser();
	}
}

using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public Vector3[] patrolPoints;
	private int currentPoint = 0;

	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	private bool triggered = false;
	private bool patroling = true;
	
	public float Speed = 10f;
	private float distance = 0.1f;
	public bool loop = false;



	void Awake(){
		this.transform.position = patrolPoints [0];
	}

	void Update(){

		Vector3 velocity = Vector3.zero;

		if (currentPoint < patrolPoints.Length) {
			Vector3 nextPoint = patrolPoints[currentPoint];
			Vector3 moveDirection = nextPoint - this.transform.position;

			velocity = this.GetComponent<Rigidbody2D>().velocity;

			if(moveDirection.magnitude < distance)
			{
				currentPoint++;
			} else {
				velocity = moveDirection.normalized * Speed;
			} 
		} else{
			if(loop)
			{
				currentPoint = 0;
			} else {
				velocity = Vector3.zero;
			}
		}

		this.GetComponent<Rigidbody2D>().velocity = velocity;

		

	}

	//Development code
	void OnDrawGizmos(){

		for (int i = 1; i < patrolPoints.Length; i++) {
			Gizmos.DrawLine(patrolPoints[i-1],patrolPoints[i]);
		}

		if (loop) {
			Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1], patrolPoints[0]);
		}

	}

}

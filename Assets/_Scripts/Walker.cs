using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public Vector3[] patrolPoints;

	private Transform[] hitBarrier;
	public LayerMask toHit;
	
	private bool triggered = false;
	private bool patroling = true;
	
	public float Speed = 1f;

	bool oneWayDone = false;



	//set up the patrolers 2d light asset
	void Start()
	{

	}

	//if player is found, and hasn't already been triggered, stop the patroling and wait a few seconds, then shoot at player
	void Update(){

		while (!oneWayDone) {


			for (int i = 1; i < patrolPoints.Length; i++) {
				if (patrolPoints [i - 1].y == patrolPoints [i].y) {
					if (patrolPoints [i - 1].x < patrolPoints [i].x) {
						StartCoroutine (move (patrolPoints [i].x, patrolPoints [i].y, true, true));
					} else {
						StartCoroutine (move (patrolPoints [i].x, patrolPoints [i].y, true, false));
					}
				} else {
					//more complicated code...
				}
			}

			Debug.Log(oneWayDone.ToString());

		}

		/*while (oneWayDone) {

			for (int i = patrolPoints.Length - 1; i > 0; i--) {
				if (patrolPoints [i].y == patrolPoints [i - 1].y) {
					if (patrolPoints [i].x < patrolPoints [i - 1].x) {
						StartCoroutine (move (patrolPoints [i - 1].x, patrolPoints [i - 1].y, true, true));
					} else {
						StartCoroutine (move (patrolPoints [i - 1].x, patrolPoints [i - 1].y, true, false));
					}
				} else {
					//more complicated code...
				}
			}
		}*/
	}


	/// <summary>
	/// move the walked from any position along a fixed axis 
	/// 	/// </summary>
	/// <param name="xEnd">X ending location.</param>
	/// <param name="yEnd">Y ending location.</param>
	/// <param name="toX">If set to <c>true</c> then the walked is moveing along x, if not
	/// the walker is moving along y.</param>
	/// <param name="toLeftOrUp">If set to <c>true</c> then the walker is moving to either
	/// left or up depending on the variable toX.</param>
	IEnumerator move(float xEnd, float yEnd, bool toX, bool toLeftOrUp){

		if (toX && toLeftOrUp) {
			while(this.transform.position.x < xEnd){
				this.transform.Translate(new Vector2(Speed * 0.01f, 0f));
				yield return null;
			}
		} else if (toX && !toLeftOrUp) {
			while(this.transform.position.x > xEnd){
				this.transform.Translate(new Vector2(-Speed * 0.01f, 0f));
				yield return null;
			}
		} else if (!toX && toLeftOrUp) {

			yield return null;
		} else if (!toX && !toLeftOrUp) {

			yield return null;
		}

		oneWayDone = !oneWayDone;
	}


	//Development code
	void OnDrawGizmos(){

		for (int i = 1; i < patrolPoints.Length; i++) {
			Gizmos.DrawLine(patrolPoints[i-1],patrolPoints[i]);
		}

	}

}

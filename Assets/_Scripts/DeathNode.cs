using UnityEngine;
using System.Collections;

public class DeathNode : MonoBehaviour {
	
	private bool Death = false;

	private GameObject canvas;
	private bool godMode = false;

	public float waitTime = 0.5f;


	void Start(){
		canvas = GameObject.FindGameObjectWithTag ("Canvas");

	}
	
	// Update is called once per frame
	void Update () {
		if (Death && !godMode) {
			StartCoroutine("waitDeath");

			if(Input.GetKeyDown(KeyCode.R) && canvas.GetComponent<Canvas>().enabled == true){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1);
	}
	
	public void setDeath(bool value){
		if (!godMode) {
			Death = value;
		}
	}

	public bool getDeath(){
		return Death;
	}

	public void setGodMode(bool god){
		godMode = god;

	}

	private IEnumerator waitDeath(){
		yield return new WaitForSeconds(waitTime);
		canvas.GetComponent<Canvas>().enabled = true;
	}
}


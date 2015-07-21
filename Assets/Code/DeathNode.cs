using UnityEngine;
using System.Collections;

public class DeathNode : MonoBehaviour {
	
	private bool Death = false;

	private GameObject canvas;

	public float waitTime = 0.5f;


	void Start(){
		canvas = GameObject.FindGameObjectWithTag ("Canvas");

	}
	
	// Update is called once per frame
	void Update () {
		if (Death) {
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
		Death = value;
	}

	public bool getDeath(){
		return Death;
	}

	private IEnumerator waitDeath(){
		yield return new WaitForSeconds(waitTime);
		canvas.GetComponent<Canvas>().enabled = true;
	}
}


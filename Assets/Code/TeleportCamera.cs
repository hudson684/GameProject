using UnityEngine;
using System.Collections;

public class TeleportCamera : MonoBehaviour {

	private GameObject mainCamera;

	public float camsize = 5;

	public float camSpeed = 10f;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player") {

			StartCoroutine("moveCam");
		} 
	}


	IEnumerator moveCam(){
		float t = 0.0f;
		Vector3 startPos = mainCamera.transform.position;
		float prevSize = mainCamera.GetComponent<Camera>().orthographicSize;

		float moveSpeed = camSpeed / 200f;

		while(t < 1.0f){

			t += moveSpeed;

			mainCamera.transform.position = Vector3.Lerp (startPos, this.transform.position, t);
			mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp (prevSize, camsize, t);
			

			yield return null;
		}
		
		
	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(camsize/4.5f * 16, camsize/4.5f * 9,0));
	}
}

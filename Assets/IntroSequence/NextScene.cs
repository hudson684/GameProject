using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	public float waitTime;
	private float adjust;

	void OnTriggerEnter2D(Collider2D other){
		adjust = waitTime;
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			if(adjust > 0){
				adjust -= Time.deltaTime;
			}else{
				Application.LoadLevel("Stage1");
			}
		}
	}
}

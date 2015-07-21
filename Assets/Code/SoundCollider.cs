﻿using UnityEngine;
using System.Collections;

public class SoundCollider : MonoBehaviour {

	public AudioClip audEnter;
	public AudioClip audExit;
	AudioSource objectAudio;




	// Use this for initialization
	void Start () {
		objectAudio = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("should work");
		if (other.tag == "Player") {
			Debug.Log("should work");
			objectAudio.PlayOneShot(audEnter);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			objectAudio.PlayOneShot(audExit);
		}
	}
}
using UnityEngine;
using System.Collections;

public class soundTriggered : MonoBehaviour {
	/// <summary>
	/// The direction you want the zone to push an object towards
	///	positive y means up, positive x means right. eg
	/// 4, 0 means pushing the object up, -1, -1 means pushing the object toward the bottom left
	/// 
	///</summary>
	public AudioClip soundEnter;
	public AudioClip soundExit;
	public AudioClip soundStay;
	AudioSource objectAudio;

	public bool enter;
	public bool exit;
	public bool stay;

	/// <summary>
	/// The max distance you want your sound to scale to 
	/// this can be both larger or smaller than the total size of half the trigger 
	/// (since we are generally working from the middle point) but if it is smaller
 	/// than the trigger then you will only have it scale to a certain way and be zero the rest of the time
	/// </summary>
	public float scale = 10f;


	void Start () {
		objectAudio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other){

		if (enter) {
			if (other.tag == "Player" && !objectAudio.isPlaying) {
				objectAudio.PlayOneShot (soundEnter);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		
		if (exit) {
			if (other.tag == "Player" && !objectAudio.isPlaying) {
				objectAudio.PlayOneShot (soundExit);
			}
		}
	}


	void OnTriggerStay2D(Collider2D other){

		float dis = Vector3.Distance (this.transform.position, other.transform.position);
		float temp = (dis / scale);

		Debug.Log ("Distance: " + dis.ToString ());

		if (temp > 1f) {
			temp = 1f;
		}

		float soundScale = 1f - temp;

		Debug.Log ("soundScale:  " + soundScale.ToString ());
		
		if (stay) {
			if (other.tag == "Player" && !objectAudio.isPlaying) {
				objectAudio.PlayOneShot (soundStay, soundScale);
			}
		}
	}
}

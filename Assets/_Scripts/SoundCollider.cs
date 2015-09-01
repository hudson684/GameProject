using UnityEngine;
using System.Collections;

public class SoundCollider : MonoBehaviour {

	public AudioClip audEnter;
	public AudioClip audExit;
	public AudioClip audLocked;
	AudioSource objectAudio;

	private Lock lockComponent;


	// Use this for initialization
	void Start () {
		objectAudio = GetComponent<AudioSource>();
		if(GetComponentInParent<Lock>()){
			lockComponent = GetComponentInParent<Lock>();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !objectAudio.isPlaying) {

			if(lockComponent.checkKeys()){
				objectAudio.Stop ();
				objectAudio.PlayOneShot(audLocked);
			}else{
				objectAudio.Stop ();
				objectAudio.PlayOneShot(audEnter);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player" && !objectAudio.isPlaying) {
			if(!lockComponent.checkKeys()){
				objectAudio.Stop ();
				objectAudio.PlayOneShot(audExit);
			}
		}
	}
}

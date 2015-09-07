using UnityEngine;
using System.Collections;

public class blastParticles : MonoBehaviour {

	public ParticleSystem particles;
	private Lock parentLock;

	void Start(){
		parentLock = GetComponentInParent<Lock>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(!parentLock.checkKeys()){
			if(other.tag == "Player"){
				particles.Play();
			}
		}
	}
}

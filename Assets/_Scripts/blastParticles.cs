using UnityEngine;
using System.Collections;

public class blastParticles : MonoBehaviour {

	public ParticleSystem particles;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			particles.Play();
		}
	}
}

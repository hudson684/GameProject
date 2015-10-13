using UnityEngine;
using System.Collections;

public class DestroyParticleSystem : MonoBehaviour {
	private ParticleSystem myParticles;
	// Use this for initialization
	void Start () {
		myParticles = gameObject.GetComponentInChildren<ParticleSystem>();
		Destroy (gameObject,myParticles.duration);
	}

}

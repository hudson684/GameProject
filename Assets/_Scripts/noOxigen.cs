using UnityEngine;
using System.Collections;

public class noOxigen : MonoBehaviour {


	/// <summary>
	/// The time till the player dies in WHOLE seconds
	/// </summary>
	public int timeTillDeath;

	private GameObject deathNodeObject;
	private DeathNode deathNode;
	public AudioClip soundBreath;
	private AudioSource objectAudio;


	// Use this for initialization
	void Awake () {

		//initialize contact with the deathnode
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponent(typeof(DeathNode));

		objectAudio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {
			Debug.Log("start death");
			StartCoroutine("slowDeath");

		}


	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			StopCoroutine ("slowDeath");
		}
	}


	IEnumerator slowDeath(){


		for (int i = 0; i <= timeTillDeath; i++) {
			yield return new WaitForSeconds(0.3f);

			if(i > (timeTillDeath / 2)){

				objectAudio.PlayOneShot(soundBreath);
			}
			yield return new WaitForSeconds(0.3f);
			
			if(i > timeTillDeath - (timeTillDeath / 3)){
				
				objectAudio.PlayOneShot(soundBreath);
			}

			yield return new WaitForSeconds(0.3f);
			Debug.Log("death part: " + i.ToString());
			objectAudio.PlayOneShot(soundBreath);

			if(i == timeTillDeath){
				deathNode.setDeath(true);
			}

		}
	}

}

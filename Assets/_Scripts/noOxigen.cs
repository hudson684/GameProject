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

	public int firstTime = 2;
	public int secondTime = 4;

	//For Drone Track
	private AudioSource droneSource;
	public AudioClip breathing;
	public AudioClip drone;

	//For Random Sounds
	private GameObject randomAudioGenerator;

	// Use this for initialization
	void Awake () {

		//initialize contact with the deathnode
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponent(typeof(DeathNode));

		objectAudio = GetComponent<AudioSource>();
		droneSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

		randomAudioGenerator = GameObject.Find("SoundEffects");

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {
			Debug.Log("start death");
			StartCoroutine("slowDeath");
			droneSource.clip = breathing;
			randomAudioGenerator.SetActive(false);
		}


	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			StopCoroutine ("slowDeath");
			droneSource.clip = drone;
			droneSource.Play();
			randomAudioGenerator.SetActive(true);
		}
	}


	IEnumerator slowDeath(){


		for (int i = 0; i <= timeTillDeath; i++) {
			yield return new WaitForSeconds(0.3f);

			if(i > timeTillDeath - (timeTillDeath / firstTime)){

				objectAudio.PlayOneShot(soundBreath);
			}
			yield return new WaitForSeconds(0.3f);
			
			if(i > timeTillDeath - (timeTillDeath / secondTime)){
				
				objectAudio.PlayOneShot(soundBreath);
				if(!droneSource.isPlaying){
					droneSource.Play();
				}
			}

			yield return new WaitForSeconds(0.3f);
			Debug.Log("death part: " + i.ToString());
			objectAudio.PlayOneShot(soundBreath);

			if(i == timeTillDeath){
				droneSource.Pause();
				deathNode.setDeath(true);
			}

		}
	}

}

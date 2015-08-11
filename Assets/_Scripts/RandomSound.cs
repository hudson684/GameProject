using UnityEngine;
using System.Collections;

public class RandomSound : MonoBehaviour {


	public bool randomTime = false;
	private AudioSource soundMaker;
	public AudioClip[] audioList;
	public Random rand;

	public int randomMin;
	public int randomMax;
	public float normalWaitTime = 30f;

	private bool playing = false;

	// Use this for initialization
	void Start () {

		soundMaker = this.GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!playing) {
			playing = true;
			StartCoroutine ("rngSound");
		}

	}


	private IEnumerator rngSound(){
		if (!soundMaker.isPlaying) {

			soundMaker.PlayOneShot(audioList[Random.Range(0, (audioList.Length-1))]);

		}


		if (randomTime) {
			yield return new WaitForSeconds ((float)Random.Range (randomMin, randomMin));
		} else {
			yield return new WaitForSeconds (normalWaitTime);
		}

		playing = false;
	}
}








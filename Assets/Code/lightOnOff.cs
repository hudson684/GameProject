using UnityEngine;
using System.Collections;

public class lightOnOff : MonoBehaviour {


	//this script toggles the light on and off at a set interval 

	bool lightOn = true;
	private Light thisLight;
	bool finishedLoop = true;

	public bool isRandom = false;

	public float lightTime = 3f;

	// Update is called once per frame
	void Update () {
		thisLight = this.GetComponent<Light>();


		//if the IEnumerator has finished, run it again at the time needed;
		if (finishedLoop) {
			finishedLoop = false;
			if(isRandom){
				float randTime = Random.value/4.0f;
				Debug.Log(randTime.ToString());
				StartCoroutine(onOff(randTime));
			} else {
				StartCoroutine (onOff(lightTime));
			}
		}

		//toggle the light
		thisLight.enabled = lightOn;
	
	}


	//wait a few seconds, then toggle the bool for the light
	IEnumerator onOff(float time){
		yield return new WaitForSeconds (time);
		lightOn = !lightOn;
		finishedLoop = true;
	}
}

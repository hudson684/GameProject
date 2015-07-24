using UnityEngine;
using System.Collections;

public class objectOnOff : MonoBehaviour {
	bool ObjectOn = true;
	private SpriteRenderer thisObject;
	bool finishedLoop = true;
	
	public bool isRandom = false;
	
	public float flipTime = 3f;
	
	// Update is called once per frame
	void Update () {
		thisObject = gameObject.GetComponent<SpriteRenderer> ();
		
		
		//if the IEnumerator has finished, run it again at the time needed;
		if (finishedLoop) {
			finishedLoop = false;
			if(isRandom){
				float randTime = Random.value/8.0f;
				Debug.Log(randTime.ToString());
				StartCoroutine(onOff(randTime));
			} else {
				StartCoroutine (onOff(flipTime));
			}
		}
		
		//toggle the light
		thisObject.enabled = ObjectOn;
		
	}
	
	
	//wait a few seconds, then toggle the bool for the light
	IEnumerator onOff(float time){
		yield return new WaitForSeconds (time);
		ObjectOn = !ObjectOn;
		finishedLoop = true;
	}
}

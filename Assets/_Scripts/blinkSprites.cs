using UnityEngine;
using System.Collections;

public class blinkSprites : MonoBehaviour {


	public float blinkTime;
	private bool boolean = true;

	public Sprite On;
	public Sprite Off;

	// Update is called once per frame
	void Update () {
		StartCoroutine ("blink");

		if (boolean) {
			this.GetComponent<SpriteRenderer>().sprite = On;
		} else {
			this.GetComponent<SpriteRenderer>().sprite = Off;
		}
	}

	IEnumerator blink(){
		yield return new WaitForSeconds(blinkTime);
		boolean = !boolean;
	}

}

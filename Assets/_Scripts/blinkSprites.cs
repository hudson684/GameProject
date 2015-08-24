using UnityEngine;
using System.Collections;

public class blinkSprites : MonoBehaviour {


	public float blinkTime;
	private bool boolean = true;
	private bool current = false;

	public Sprite On;
	public Sprite Off;

	// Update is called once per frame
	void Update () {

		if (!current) {
			current = true;
			StartCoroutine ("blink");
		}

		if (boolean) {
			this.GetComponent<SpriteRenderer>().sprite = On;
		} else {
			this.GetComponent<SpriteRenderer>().sprite = Off;
		}
	}

	IEnumerator blink(){
		yield return new WaitForSeconds(blinkTime);
		boolean = !boolean;
		current = false;
	}

}

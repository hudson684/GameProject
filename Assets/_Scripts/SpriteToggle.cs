using UnityEngine;
using System.Collections;

public class SpriteToggle : MonoBehaviour {


	SpriteRenderer rend;

	public Sprite start;
	public Sprite triggered;

	// Use this for initialization
	void Start () {

		rend = this.GetComponent<SpriteRenderer> ();
		rend.sprite = start;
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			if(Input.GetButtonDown("Interact")){
				rend.sprite = triggered;
			}
		}
	}
}

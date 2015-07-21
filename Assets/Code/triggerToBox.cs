using UnityEngine;
using System.Collections;

public class triggerToBox : MonoBehaviour {

	public GameObject listener;
	private bool triggered = false;
	public Sprite NotActive;
	public Sprite Active;
	private SpriteRenderer spriteRenderer; 

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
			spriteRenderer.sprite = NotActive; // set the sprite to sprite1
	}


	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("trigger sent");

		if (!triggered) {
			triggered = !triggered;
			spriteRenderer.sprite = Active;
			listener.SetActive(false);

		}
	}
}

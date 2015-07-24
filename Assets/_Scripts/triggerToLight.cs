using UnityEngine;
using System.Collections;

public class triggerToLight : MonoBehaviour {

	public GameObject listener;
	private bool triggered = false;
	public Sprite NotActive;
	public Sprite Active;
	private SpriteRenderer spriteRenderer; 
	private AICamera cam;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
			spriteRenderer.sprite = NotActive; // set the sprite to sprite1
	
		cam = (AICamera) listener.GetComponentInParent(typeof(AICamera));
	}


	void OnTriggerStay2D(Collider2D other){
		Debug.Log ("trigger sent");
		if(Input.GetKeyDown(KeyCode.F)){
			if (!triggered) {
				triggered = true;
				spriteRenderer.sprite = NotActive;
				cam.isActive(false);
			} else {
				triggered = false;
				spriteRenderer.sprite = Active;
				cam.isActive(true);
			}
		}
	}
}

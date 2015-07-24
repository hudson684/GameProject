using UnityEngine;
using System.Collections;

public class ManualOccluder : MonoBehaviour {

	private SpriteRenderer[] spriteComponents;
	private Color trans = new Color(0,0,0,0);

	// Use this for initialization
	void Start () {
		spriteComponents = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			toggleOff();
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if(other.tag == "Player"){
			toggleOn();
		}
	}

	void toggleOff(){
			for(int i = 0; i < spriteComponents.Length; i++){
			spriteComponents[i].enabled = false;
			}
		}


	void toggleOn(){
		for(int i = 0; i < spriteComponents.Length; i++){
			spriteComponents[i].enabled = true;
		}
	}
}

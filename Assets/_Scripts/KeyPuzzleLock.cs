using UnityEngine;
using System.Collections;

public class KeyPuzzleLock : MonoBehaviour {

	private GameObject child;
	private KeyPuzzleKey key;
	public int code;

	private bool unlocked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (child != null) {
			key = (KeyPuzzleKey) child.GetComponent(typeof(KeyPuzzleKey));

			if(key.getCode() == code){
				child.transform.SetParent(this.transform);
				unlocked = true;
			}
		}
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "KeyPuzzleKey") {
			Debug.Log("the key is in the hole");
			child = other.gameObject;
		}

	}

	public bool getUnlocked(){

		return unlocked;
	}




}

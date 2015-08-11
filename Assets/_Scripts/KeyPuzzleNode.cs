using UnityEngine;
using System.Collections;

public class KeyPuzzleNode : MonoBehaviour {

	
	private bool unlocked = false;

	public GameObject[] locks;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool temp = false;
		KeyPuzzleLock tempLock;

		for (int i = 0; i < locks.Length; i++) {
			tempLock = (KeyPuzzleLock) locks[i].GetComponent(typeof(KeyPuzzleLock));

			if(!tempLock.getUnlocked()){
				temp = true;
			}
		}

		unlocked = temp;
		Debug.Log ("Unlocked: " + unlocked);
	}
	

	public bool getUnlocked(){

		return unlocked;
	}

	public void setUnlocked(bool boolean){

		unlocked = boolean;
	}



}

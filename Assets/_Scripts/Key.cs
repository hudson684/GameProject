using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	private bool isLocked = true;

	public void Unlock(){
		isLocked = false;
	}

	public bool getLocked(){
		return isLocked;
	}
}

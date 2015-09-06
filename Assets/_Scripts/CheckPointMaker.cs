using UnityEngine;
using System.Collections;

public class CheckPointMaker : MonoBehaviour {

	private static CheckPointMaker cp;

	void Awake() {
		if (cp == null) {
			DontDestroyOnLoad (this.gameObject);
			cp = this;
		} else if (cp != this) {
			Destroy(this.gameObject);
		}
	}
}

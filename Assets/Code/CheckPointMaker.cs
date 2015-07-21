using UnityEngine;
using System.Collections;

public class CheckPointMaker : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(this);
	}
}

using UnityEngine;
using System.Collections;

public class ClickToContinue : MonoBehaviour {
	public int levelIndex;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Application.LoadLevel(levelIndex);
		}
	}
}

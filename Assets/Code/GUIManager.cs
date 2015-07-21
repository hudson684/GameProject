using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public Canvas deathGUI;
	public bool debugger;

	void Update(){
		if(debugger){
			runDeathGUI ();
		}
	}

	void runDeathGUI(){
		deathGUI.enabled = true;
	}

}

using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public void loadNextScene(){
		Application.LoadLevel("Stage1");
	}

	public void exitGame(){
		Application.Quit();
	}

}

using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public void loadNextScene(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void exitGame(){
		Application.Quit();
	}

	public void loadSave(){
		Application.LoadLevel(PlayerPrefs.GetString("SaveLevelKey"));
	}

}

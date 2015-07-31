using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

	public bool nextLevel;
	public float delay;

	private bool start = false;
	private PlayerControl control;
	private int levelIndex;

	void Start(){
		control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		levelIndex = Application.loadedLevel;
	}
	

	void Update(){
		if(start){
			startTimer();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			control.enabled = false;
			start = true;
		}
	}

	void startTimer(){
			if(nextLevel){
				nextScene();
			}else{
				lastScene();
			}
		}

	void nextScene(){
		if(delay > 0){
			delay -= Time.deltaTime;
		}else{
			Application.LoadLevel(levelIndex + 1);
		}
	}

	void lastScene(){
		if(delay > 0){
			delay -= Time.deltaTime;
		}else{
			Application.LoadLevel(levelIndex - 1);
		}
	}
}

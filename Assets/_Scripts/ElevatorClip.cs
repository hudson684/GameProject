using UnityEngine;
using System.Collections;

public class ElevatorClip : MonoBehaviour {
	
	private Animator elevAnim;
	public GameObject newPos;
	private int cutsceneHash = Animator.StringToHash("Cutscene");

	// Use this for initialization
	void Start () {
		elevAnim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void triggerCutscene(){
		elevAnim.SetTrigger(cutsceneHash);
	}

	public void decouple(){
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		Player.transform.parent = null;
		Player.transform.position = newPos.transform.position;
		Player.GetComponent<PlayerControl>().enabled = true;
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.transform.parent = null;
	}
}

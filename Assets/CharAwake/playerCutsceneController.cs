using UnityEngine;
using System.Collections;

public class playerCutsceneController : MonoBehaviour {

	Animator anim;
	int standHash = Animator.StringToHash("Stand");

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			anim.SetBool(standHash, true);
		}

	}
}

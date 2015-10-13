using UnityEngine;
using System.Collections;

public class DummyControls : MonoBehaviour {

	private Animator anim;
	private int upHash = Animator.StringToHash("getUp");
	public Canvas titleCard;

	private bool startTimer = false;
	public float timeR = 3;

	void Start(){
		anim = gameObject.GetComponent<Animator>();
		titleCard.enabled = false;
	}

	void Update(){
		if(Input.anyKey){
			anim.SetBool(upHash, true);
		}
		if(startTimer){
			if(timeR > 0 ){
				countDown();
			}else{
				Application.LoadLevel(Application.loadedLevel+1);
			}
		}
	}

	void openCanvas(){
		titleCard.enabled = true;
		startTimer = true;
	}

	void countDown(){
		timeR -= Time.deltaTime;
	}
}

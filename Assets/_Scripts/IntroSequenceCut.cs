using UnityEngine;
using System.Collections;

public class IntroSequenceCut : MonoBehaviour {

	public GameObject[] GLrigs;
	public DummyControls dummy;


	// Use this for initialization
	void Start () {
		foreach(GameObject rig in GLrigs){
			rig.SetActive(false);
		}
	}
	
	public void clearRigs(){
		foreach(GameObject rig in GLrigs){
			rig.SetActive(false);
		}
	}

	public void useRig(int i){
		clearRigs();
		GLrigs[i].SetActive(true);
	}

	public void enableDummy(){
		dummy.enabled = true;
	}
}

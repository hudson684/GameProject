using UnityEngine;
using System.Collections;

public class TempLight : MonoBehaviour {


	public GameObject theLock;
	public Material locked;
	public Material unlocked;

	private KeyPuzzleLock tempLock;
	private bool isUnlocked;

	//private Renderer myrenderer;
	public MeshRenderer myrenderer;

	// Use this for initialization
	void Start () {
	
		tempLock = (KeyPuzzleLock) theLock.GetComponent(typeof(KeyPuzzleLock));
		//Renderer myrenderer = this.GetComponent<MeshRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (tempLock.getUnlocked ()) {
			//rendere
			myrenderer.material = unlocked;

		} else {
			myrenderer.material = locked;
		}
	
	}
}

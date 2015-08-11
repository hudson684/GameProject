using UnityEngine;
using System.Collections;

public class tempFinalLight : MonoBehaviour {

	public GameObject theLock;
	public Material locked;
	public Material unlocked;
	
	private KeyPuzzleNode tempLock;
	private bool isUnlocked;
	
	//private Renderer myrenderer;
	public MeshRenderer myrenderer;
	
	// Use this for initialization
	void Start () {
		
		tempLock = (KeyPuzzleNode) theLock.GetComponent(typeof(KeyPuzzleNode));
		//Renderer myrenderer = this.GetComponent<MeshRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (tempLock.getUnlocked()) {
			//rendere
			myrenderer.material = unlocked;
			
		} else {
			myrenderer.material = locked;
		}
		
	}
}

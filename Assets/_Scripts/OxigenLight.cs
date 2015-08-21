using UnityEngine;
using System.Collections;

public class OxigenLight : MonoBehaviour {


	public Material normal;
	public Material warning;
	public Material danger;
	public Material off;

	private int threatLevel;

	public int NORMAL = 0;
	public int MEDIUM = 1;
	public int HIGH = 2;
	public int INSANE = 3;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (threatLevel == NORMAL) {
			Debug.Log("normal");
		} else if (threatLevel == MEDIUM) {
			Debug.Log("medium");
		} else if (threatLevel == HIGH) {
			Debug.Log("high");
		} else if (threatLevel == INSANE) {
			Debug.Log("ultra");
		}

	}


	public void setThreatLevel( int threat){
		threatLevel = threat;
	}


}

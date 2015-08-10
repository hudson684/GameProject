using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

	public Key refkey;
	public Material red,green;
	private Renderer indComponent;

	// Use this for initialization
	void Start () {
		indComponent = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//assess if instance has a reference key
		if(refkey != null){//if reference key is set check if locked
			if(!refkey.getLocked()){
				indComponent.material = green;
			}else{
				indComponent.material = red;
			}
		}else{
			//if reference key not set, default to green
			indComponent.material = green;
		}
	}

	public void setKey(Key key){
		refkey = key;
	}
}

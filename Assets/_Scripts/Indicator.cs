using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

	public Key refkey;
	public Sprite red,green;
	private SpriteRenderer indComponent;

	// Use this for initialization
	void Start () {
		indComponent = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//assess if instance has a reference key
		if(refkey != null){//if reference key is set check if locked
			if(!refkey.getLocked()){
				indComponent.sprite = green;
			}else{
				indComponent.sprite = red;
			}
		}else{
			//if reference key not set, default to green
			indComponent.sprite = green;
		}
	}

	public void setKey(Key key){
		refkey = key;
	}
}

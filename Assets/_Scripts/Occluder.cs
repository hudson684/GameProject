using UnityEngine;
using System.Collections;

public class Occluder : MonoBehaviour {

	private SpriteRenderer rendererComponent;
	private OcclusionManager manager;
	private Color full = new Color(0,0,0,255);

	private bool toggle = true;
	public bool Toggle{
		set{
			toggle = value;
		}
	}

	// Use this for initialization
	void Start () {
		rendererComponent = gameObject.GetComponent<SpriteRenderer>();
		manager = GetComponentInParent<OcclusionManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(toggle){
			if(rendererComponent.color.a < 0.95){
				//Debug.Log ("toggle on "+ rendererComponent.color.a);
				rendererComponent.color = Color.Lerp(rendererComponent.color, Color.black, Time.deltaTime * manager.dampeningFrom);
			}else{
				rendererComponent.color = Color.black;
			}
		}else{
			if(rendererComponent.color.a > 0.05){
				//Debug.Log ("toggle off "+ rendererComponent.color.a);
				rendererComponent.color = Color.Lerp(rendererComponent.color, Color.clear, Time.deltaTime * manager.dampeningTo);
			}else{
				rendererComponent.color = Color.clear;
			 }
		}
	}
}

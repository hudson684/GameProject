using UnityEngine;
using System.Collections;

public class slowFade : MonoBehaviour {

	private CanvasRenderer Panel;
	private float nextAlpha;
	private ClickToContinue controls;

	// Use this for initialization
	void Start () {
		Panel = GameObject.Find("Panel").GetComponent<CanvasRenderer>();
		controls = GameObject.Find("Controls").GetComponent<ClickToContinue>();
		controls.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Panel.GetAlpha() > 0.08f){
			nextAlpha = Mathf.Lerp(Panel.GetAlpha(), 0f, Time.deltaTime*0.2f);
			Panel.SetAlpha(nextAlpha);
		}else{
			Panel.SetAlpha(0);
			controls.enabled = true;
		}

	}
}

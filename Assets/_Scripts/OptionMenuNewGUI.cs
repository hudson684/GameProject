using UnityEngine;
using System.Collections;

public class OptionMenuNewGUI : MonoBehaviour {

	private bool min = false;
	private bool low = false;
	private bool normal = true;
	private bool high = false;

	private float totalVol = 0.5f;

	private int AA = 4;

	private bool titleMenu = false;
	private Canvas canvas;


	// Use this for initialization
	void Start () {
		GameObject pause = GameObject.FindGameObjectWithTag ("PauseMenu");

		canvas = GameObject.FindGameObjectWithTag("OptionsMenu").GetComponent<Canvas>();

		if (pause == null) {
			Debug.Log("In title menu");
			titleMenu = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void open(){
		Debug.Log ("pressed open");
		canvas.enabled = true;

	}

	public void close(){
		canvas.enabled = false;
	}


	public void volChange(float volume){
		totalVol = volume;
	}

	public void zeroAA(){
		AA = 0;
	}
	public void twoAA(){
		AA = 2;
	}
	public void fourAA(){
		AA = 4;
	}
	public void eightAA(){
		AA = 8;
	}

	public void setBoolLow(){
		low = true;
		min = false;
		high = false;
		normal = false;

	}

	public void setBoolMin(){
		low = false;
		min = true;
		high = false;
		normal = false;

	}

	public void setBoolNormal(){
		low = false;
		min = false;
		high = false;
		normal = true;

		
	}
	
	public void setBoolHigh(){
		low = false;
		min = false;
		high = true;
		normal = false;
		
	}

	public void settingChange(){

		//Debug.Log("Min: " + min.ToString() + "Low: " + low.ToString() + "Normal: " + normal.ToString() + "High: " + high.ToString());

		//QualitySettings.antiAliasing = AA;

		AudioListener.volume = totalVol;
		Debug.Log (AudioListener.volume.ToString ());


		if (min) {
			QualitySettings.SetQualityLevel(0, false);

		} else if (low) {
			QualitySettings.SetQualityLevel(1, false);

		} else if (normal) {
			QualitySettings.SetQualityLevel(3, false);

		} else if (high){
			QualitySettings.SetQualityLevel(5, false);
		}




		Debug.Log("Quality setting: " + QualitySettings.GetQualityLevel().ToString());

	}

}

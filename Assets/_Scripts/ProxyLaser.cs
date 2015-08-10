using UnityEngine;
using System.Collections;

public class ProxyLaser : Laser {

	public float toggleDistance;
	public float delay;
	private float adjust;

	// Use this for initialization
	void Start () {
		setupLaser();
	}
	
	// Update is called once per frame
	void Update () {
		traceLaser();
		proxyToggle();
	}

	/// <summary>
	/// Toggles lasers on and off depending on distance of objects to laser
	/// </summary>
	void proxyToggle(){
		//Error controll
		//Prevents null exception erros
		if(GameObject.FindGameObjectWithTag("Object")){
			
			//find objects with the tag "Object"
			GameObject[] Boxs = GameObject.FindGameObjectsWithTag("Object");
			
			//calculate distance of object to Laser
			for(int i = 0; i < Boxs.Length; i++){
				float distance = Vector2.Distance(transform.position, Boxs[i].transform.position);
				if(distance > toggleDistance){
					if(adjust < 0){
						toggleOn();
					}else{
						adjust-= Time.deltaTime;
					}
					
				}else{
					toggleOff();
					adjust = delay;
				}//end if
			}//Toggle laser on and off if box within toggle distance
		}//end if
	}//end laserToggle()
}

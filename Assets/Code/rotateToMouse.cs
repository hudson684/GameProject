using UnityEngine;
using System.Collections;

public class rotateToMouse : MonoBehaviour {
	
	
	public float MAX_ANGLE = 359;
	public float MIN_ANGLE = 180;
	// Update is called once per frame
	void Update () 
	{
		Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
		Vector3 objpos = Camera.main.WorldToViewportPoint (transform.position);        //Object position on screen
		Vector2 relobjpos = new Vector2(objpos.x - 0.5f,objpos.y - 0.5f);            //Set coordinates relative to object
		Vector2 relmousepos = new Vector2 (mouse.x - 0.5f,mouse.y - 0.5f) - relobjpos;
		float angle = Vector2.Angle (Vector2.up, relmousepos);    //Angle calculation
		if (relmousepos.x > 0) {
			angle = 360 - angle;
		}

		if (angle >= MAX_ANGLE || angle < 90) {
			angle = MAX_ANGLE;
		}
		if (angle <= MIN_ANGLE && angle >= 90) {
			angle = MIN_ANGLE;
		}
		Quaternion quat = Quaternion.identity;
		quat.eulerAngles = new Vector3(0,0,angle); //Changing angle
		transform.rotation = quat;
	}
}
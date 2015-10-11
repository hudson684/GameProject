using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	
	//set the gravity 
	private const int FULL_GRAVITY = 2;
	private const int ZERO_GRAVITY = 0;
	
	public float edgeBuffer = 3f;
	private GameObject cursor;
	
	private bool follow;
	
	private int camType = 0;
	
	private GameObject deathNodeObject;
	private ControlNode contNode;
	
	
	
	/// <summary>
	/// The minimum orthegraphic size (look at size on the camera component on this camera)
	/// </summary>
	public float minZoom = 3.4f;
	
	/// <summary>
	/// The max orthegraphic size (look at size on the camera component on this camera)
	/// </summary>
	public float maxZoom = 8f;
	
	private cursor cursorCode;
	
	private float zoomValue;
	
	
	void Awake(){
		cursor = GameObject.FindGameObjectWithTag ("Cursor");
		cursorCode = (cursor) cursor.GetComponent(typeof(cursor));
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		contNode = (ControlNode) deathNodeObject.GetComponent(typeof(ControlNode));
		
		zoomValue = minZoom;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//if in zome mode, change the zoom of the camera based on
		//the position of the mouse, starting at 1/10th (from origin of player)
		//all the way out to the screen edge. Keep this to the min and max zoom of the 
		//camera set in public variables.
		
		//at all times position player 1/3rd of the 
		//camera based on the direction the player is facing
		
		
		Vector3 target = contNode.getPlayerPosition ();
		Vector3 point = this.GetComponent<Camera>().WorldToViewportPoint(target);
		Vector3 delta = target - this.GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z)); 
		
		
		if (contNode.getPlayerGrav() == FULL_GRAVITY) {
			if (contNode.getPlayerFacingLeft ()) {
				delta = target - this.GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.7f, 0.3f, point.z));
			} else {
				delta = target - this.GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.3f, 0.3f, point.z));
			}
			
		}
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, -10f), ref velocity, dampTime);
		
		
		
		
		if(Input.GetKey(KeyCode.LeftShift)){
			Debug.Log("is shifting");
			Vector3 cursorCamSpace = this.GetComponent<Camera>().WorldToViewportPoint(cursor.transform.position);
			Vector3 targetCamSpace = this.GetComponent<Camera>().WorldToViewportPoint(target);
			
			float dis = Vector3.Distance(cursorCamSpace, targetCamSpace);
			
			Debug.Log("Distance from player: " + dis.ToString());
			
			zoomValue = Mathf.Lerp(minZoom, maxZoom, (dis-1f));
		}
		
		
		
		this.GetComponent<Camera>().orthographicSize = zoomValue;
		
		
	}
}
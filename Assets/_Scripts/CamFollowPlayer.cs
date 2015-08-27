using UnityEngine;
using System.Collections;

public class CamFollowPlayer : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public float distanceFromPlayer = 7f;
	public float maxDistanceFromPlayer = 32f;
	public float edgeBuffer = 3f;
	private GameObject cursor;


	public bool needToHoldShift = true;



	/// <summary>
	/// The minimum orthegraphic size (look at size on the camera component on this camera)
	/// </summary>
	public float minZoom = 3.4f;

	/// <summary>
	/// The max orthegraphic size (look at size on the camera component on this camera)
	/// </summary>
	public float maxZoom = 8f;

	private cursor cursorCode;


	void Awake(){
		cursor = GameObject.FindGameObjectWithTag ("Cursor");
		cursorCode = (cursor) cursor.GetComponent(typeof(cursor));

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = this.GetComponent<Camera>().WorldToViewportPoint(target.position);




			if(cursorCode.getDistance() < distanceFromPlayer){

				Vector3 delta = target.position - this.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
				//Vector3 delta = target.position - this.transform.position;

				Vector3 destination = transform.position + delta;
				transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, -10f), ref velocity, dampTime);
			} else {

				if(needToHoldShift){

					if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
						Vector3 targetPos = Vector3.Lerp(target.position, cursor.transform.position , 0.5f);
						
						if(cursorCode.getDistance() > 40f){
							targetPos = Vector3.Lerp(target.position, target.position , 0.5f);
						}
						
						Vector3 delta = targetPos - this.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
						
						//Vector3 delta = targetPos - this.transform.position;
						
						Vector3 destination = transform.position + delta;
						transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, -10f), ref velocity, dampTime);
						
						//7 distance = min = 3.4 cam zone
						//32 = max = 8 cam zone
						float factor = ((cursorCode.getDistance() - distanceFromPlayer) / (maxDistanceFromPlayer - distanceFromPlayer));
						
						if(factor > 1f){
							
							factor = 1f;
						}
						
						
						float zoomValue = Mathf.Lerp(minZoom, maxZoom, factor);
						
						this.GetComponent<Camera>().orthographicSize = zoomValue;
					}

				} else {
					Vector3 targetPos = Vector3.Lerp(target.position, cursor.transform.position , 0.5f);
					
					if(cursorCode.getDistance() > 40f){
						targetPos = Vector3.Lerp(target.position, target.position , 0.5f);
					}
					
					Vector3 delta = targetPos - this.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
					
					//Vector3 delta = targetPos - this.transform.position;
					
					Vector3 destination = transform.position + delta;
					transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y, -10f), ref velocity, dampTime);
					
					//7 distance = min = 3.4 cam zone
					//32 = max = 8 cam zone
					float factor = ((cursorCode.getDistance() - distanceFromPlayer) / (maxDistanceFromPlayer - distanceFromPlayer));
					
					if(factor > 1f){
						
						factor = 1f;
					}
					
					
					float zoomValue = Mathf.Lerp(minZoom, maxZoom, factor);
					
					this.GetComponent<Camera>().orthographicSize = zoomValue;
				}

			}


		}
		
	}
}
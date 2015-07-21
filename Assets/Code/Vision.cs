using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {

	private GameObject player;
	private RaycastHit2D hit;
	private LineRenderer vision;

	private float viewAngleAdjust;

	public float coneAngle;
	public float viewRange;
	public Vector2 playerLocationAdjust;

	// Use this for initialization
	void Start () {
		//used in targeting
		player = GameObject.FindGameObjectWithTag("Player");
		vision = GetComponent<LineRenderer>();
		vision.SetWidth(0,5);
	}
	
	// Update is called once per frame
	void Update () {
		playerLocationAdjust = transform.position - player.transform.position;
		Debug.Log (Vector2.Angle (transform.position,player.transform.position));
		findPlayer();
	}

	void findPlayer(){
		hit = Physics2D.Raycast(transform.position, -playerLocationAdjust, viewRange);
		if(hit.collider){
			if(hit.collider.tag == "Player"){
				Debug.DrawLine(transform.position, hit.point);
				float currentDistance =Vector2.Distance(transform.position, hit.point);
				viewAngleAdjust = currentDistance / 3;
				vision.SetWidth(0,viewAngleAdjust);
				vision.SetPosition(0,transform.position);
				vision.SetPosition(1, hit.point);
			}else{

			}
		}
	}


	void lateUpdate(){


	}
}

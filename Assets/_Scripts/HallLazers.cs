using UnityEngine;
using System.Collections;

public class HallLazers : MonoBehaviour {

	public float thickness = 0.2f;
	public bool TimerLaser = true;
	public float toggleDistance;
	public LayerMask hitLayers;

	private RaycastHit2D hit;
	private LineRenderer laserLine;
	private Vector2 left;

	private GameObject deathNodeObject;
	private DeathNode deathNode;

	//Controls the Toggle duration of the Toggle Lazer
	public float delay;
	private float adjust;

	//Controls the Toggle duration for the timer Laser
	public float startTime;
	public float onDuration;
	public float offDuration;
	private float onTimer;
	private float offTimer;
	private bool laserOn = true;

	// Use this for initialization
	void Start () {
		//Laser Variables
		laserLine = GetComponent<LineRenderer>();
		laserLine.SetWidth(thickness, thickness);
		left = new Vector2(-5,0);

		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponentInParent(typeof(DeathNode));

		onTimer = 0;
		offTimer = startTime;
	}
	
	// Update is called once per frame
	void Update () {
		traceLaser();
		if(TimerLaser){
			timerToggle();
		}else{
			laserToggle();
		}
	}

	/// <summary>
	/// Traces the laser to the left of the Laser origin object
	/// </summary>
	void traceLaser(){
		if(laserLine.enabled == true){
			//laser start Pos
			laserLine.SetPosition(0,transform.position);

			//find laser end point
			hit = Physics2D.Raycast(transform.position,-left,400,hitLayers);

			//if ray hits object
			if(hit.collider){
				laserLine.SetPosition(1, hit.point);
				laserEffect();
			}//end if

		//Testing
		//Debug.Log (hit.collider.tag.ToString());
		//Debug.DrawLine(transform.position, hit.point);
		}//end if
	}//end traceLaser

	/// <summary>
	/// Controls the Effect of the lasers on the player
	/// Kills the player
	/// </summary>
	void laserEffect(){
		//If collide with player, kill Player
		if(hit.collider.tag == "Player"){
			deathNode.setDeath(true);
		}//end if
	}//end laserEffect
		

	/// <summary>
	/// Toggles lasers on and off depending on distance of objects to laser
	/// </summary>
	void laserToggle(){
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
						laserLine.enabled = true;
					}else{
						adjust-= Time.deltaTime;
					}
					
				}else{
					laserLine.enabled = false;
					adjust = delay;
				}//end if
			}//Toggle laser on and off if box within toggle distance
		}//end if
	}//end laserToggle()

	/// <summary>
	/// Toggles the laser On and off according to a timer
	/// </summary>
	void timerToggle(){
		if(laserOn){
			if(onTimer > 0){
				//countdown timer
				onTimer -= Time.deltaTime;
			}else{
				//toggle laserOff
				laserLine.enabled = false;
				//Reset on Timer
				onTimer = onDuration;
				laserOn = false;
			}
		}else{
			if(offTimer > 0){
				//countdown timer
				offTimer -= Time.deltaTime;
			}else{
				//toggle laserOff
				laserLine.enabled = true;
				//Reset on Timer
				offTimer = offDuration;
				laserOn = true;
			}
		}
	}//end timer Toggle
}

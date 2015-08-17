using UnityEngine;
using System.Collections;

/// <summary>
/// Base Class for Lasers
/// </summary>
public class Laser : MonoBehaviour {

	//Public Variables
	public float thickness = 0.2f;
	public float length = 400f;
	public LayerMask hitLayers;
	public Vector2 direction;
	public AudioClip laserClip;

	//Privvate Variables
	private RaycastHit2D hit;
	private LineRenderer laserLine;
	private GameObject deathNodeObject;
	private DeathNode deathNode;
	private AudioSource audioSource;
	

	// Update is called once per frame
	void Update () {
		traceLaser();
	}

	public void setupLaser(){
		laserLine = gameObject.GetComponent<LineRenderer>();
		laserLine.SetWidth(thickness, thickness);
		
		deathNodeObject = GameObject.FindGameObjectWithTag ("DeathNode");
		deathNode = (DeathNode) deathNodeObject.GetComponentInParent(typeof(DeathNode));

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = laserClip;
	}

	/// <summary>
	/// Traces the laser to the left of the Laser origin object
	/// </summary>
	public void traceLaser(){
		if(laserLine.enabled == true){
			//laser start Pos
			laserLine.SetPosition(0,transform.position);
			
			//find laser end point
			hit = Physics2D.Raycast(transform.position,direction,length,hitLayers);
			
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


	public void toggleOn(){
		laserLine.enabled = true;
		audioSource.Play ();
	}

	public void toggleOff(){
		laserLine.enabled = false;
		audioSource.Stop();
	}	
}

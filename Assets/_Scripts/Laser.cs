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
	public GameObject sparks;

	//Privvate Variables
	private RaycastHit2D hit;
	private LineRenderer laserLine;
	private GameObject deathNodeObject;
	private DeathNode deathNode;
	private AudioSource audioSource;
	private GameObject _particles;
	[HideInInspector]public bool kill = true;

	private int flickerDelay;
	

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
		laserLine.SetWidth(thickness,thickness);
		if(laserLine.enabled == true ){
			//laser start Pos
			laserLine.SetPosition(0,transform.position);
			
			//find laser end point
			hit = Physics2D.Raycast(transform.position,direction,length,hitLayers);
			
			//if ray hits object
			if(hit.collider && kill){
				laserLine.SetPosition(1, hit.point);
				if(_particles == null){
					_particles = Instantiate(sparks,hit.point,Quaternion.LookRotation(direction + Vector2.up, -direction)) as GameObject;
				}
				_particles.transform.position = hit.point;
				_particles.transform.rotation = Quaternion.LookRotation(direction + Vector2.up,-direction);
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
		audioSource.Play ();
		laserLine.enabled = true;
	}

	public void toggleOff(){
		laserLine.enabled = false;
		audioSource.Stop();
		if(_particles != null){
			Destroy(_particles);
		}
	}

	public void traceFlicker(){
		kill = false;
		if(flickerDelay == 0){
			laserLine.enabled = !laserLine.enabled;
			laserLine.SetPosition(0,transform.position);
			laserLine.SetWidth(thickness/2,thickness/2);
			hit = Physics2D.Raycast (transform.position,direction, length, hitLayers);
			if(hit.collider){
				laserLine.SetPosition(1,hit.point);
			}
			flickerDelay = 2;
		}else{
			flickerDelay --;
		}
	}
}

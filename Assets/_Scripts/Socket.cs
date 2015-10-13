using UnityEngine;
using System.Collections;

public class Socket : Key {

	//target
	public Collider2D refCore;
	private Rigidbody2D refRigid;

	//Public Positioning
	public float transitionTime = 1.5f;
	public float transitionSpeed = 5f;

	//Positioning
	private float transitionTemp;
	private Transform refTransform;
	private bool rotate = false;
	private bool move = false;

	//Animation
	private Animator anim;
	private int closedHash = Animator.StringToHash("Close");

	//laser
	public Laser targetLaser;

	//Player and Grapple
	private GrappleControl grapple;


	void Start(){
		refTransform = transform.FindChild("DummyCore");
		refRigid = refCore.GetComponent<Rigidbody2D>();
		transitionTemp = transitionTime;
		anim = transform.FindChild("Reactor Socket").GetComponent<Animator>();
		grapple = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GrappleControl>();
	}

	void Update(){
		rotateToPosition();
		moveToPosition();

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other == refCore){
			grapple.retractGrapple();
			refCore.transform.SetParent(transform);
			Unlock();
			refRigid.Sleep();
			refCore.enabled = false;
			rotate = true;
			if(targetLaser){
				targetLaser.toggleOff();
			}
			//refCore.transform.rotation = refTransform.rotation;
			//refCore.transform.position = refTransform.position;
		}
	}

	void rotateToPosition(){
		if(rotate){
			if(transitionTemp > 0){
				refCore.transform.rotation = Quaternion.Slerp(refCore.transform.rotation,refTransform.rotation, Time.deltaTime * transitionSpeed);
				transitionTemp -= Time.deltaTime;
			}else{
				refCore.transform.rotation = refTransform.rotation;
				rotate = false;
				move = true;
				transitionTemp = transitionTime;
			}
		}
	}

	void moveToPosition(){
		if(move){
			if(transitionTemp > 0){
				refCore.transform.position = Vector3.Slerp(refCore.transform.position,refTransform.position, Time.deltaTime * transitionSpeed);
				transitionTemp -= Time.deltaTime;
			}else{
				refCore.transform.position = refTransform.position;
				move = false;
				transitionTemp = transitionTime;
				anim.SetBool(closedHash,true);
				refRigid.freezeRotation = true;
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, refCore.transform.position);
	}
}

using UnityEngine;
using System.Collections;

public class Socket : Key {

	//target
	public Collider2D refCore;

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


	void Start(){
		refTransform = transform.FindChild("DummyCore");
		transitionTemp = transitionTime;
		anim = transform.FindChild("Reactor Socket").GetComponent<Animator>();
	}

	void Update(){
		rotateToPosition();
		moveToPosition();

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other == refCore){
			Unlock();
			refCore.GetComponent<Rigidbody2D>().Sleep();
			refCore.GetComponent<BoxCollider2D>().enabled = false;
			rotate = true;
			//refCore.transform.rotation = refTransform.rotation;
			//refCore.transform.position = refTransform.position;
			refCore.transform.SetParent(transform);
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
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, refCore.transform.position);
	}
}

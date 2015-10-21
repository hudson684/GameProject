﻿using UnityEngine;
using System.Collections;

public class Mantle : MonoBehaviour {


	private bool mantling;
	private PlayerControl playerCont;
	private GameObject player;
	
	private Vector3 mantlePosition;
	private Vector3 endPosition;
	//private Vector3 mantleFall;

	//Animation Controller
	private Animator anim;
	private int isMantleingHash = Animator.StringToHash("Mantling");
	private int mantleUpHash = Animator.StringToHash("MantlePull");
	private int mantleDropHash = Animator.StringToHash("MantleDrop");

	void Awake(){
		anim = GetComponentInParent<Animator>();
	}

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
		playerCont = (PlayerControl) this.GetComponentInParent(typeof(PlayerControl));
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("mantling: " + mantling.ToString ());


		anim.SetBool(isMantleingHash, mantling);
		if (mantling){
			if(mantlePosition != null && mantlePosition != Vector3.zero)
			{
				player.transform.position = mantlePosition;
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				
				if(Input.GetKeyDown(KeyCode.W)){
					anim.SetTrigger(mantleUpHash);
				}
				
				if(Input.GetKeyDown(KeyCode.S)){
					//player.transform.position = mantleFall;
					anim.SetTrigger(mantleDropHash);
					mantling = false;
					playerCont.setMantling(mantling);
					anim.SetBool(isMantleingHash,mantling);
				}
			
			} else {
				mantling = false;
			}

		}
	}
	

	//while in the mantle area set the position and end position of the mantle
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "MantleCorner" && !mantling) {
			if(Input.GetKeyDown(KeyCode.W)){
				Debug.Log("should mantle");

				Debug.Log(other.transform.position.ToString());
				mantlePosition = other.transform.position;

				Debug.Log("other position: " + other.transform.position.ToString());
				Debug.Log("other name: " +other.name.ToString());
				Debug.Log("mantle position: " + mantlePosition.ToString());

				Transform child = other.transform.GetChild(0);
				//Transform child2 = other.transform.GetChild(1);

				endPosition = child.transform.position;
				//mantleFall = child2.transform.position;

				mantling = true;
				playerCont.setMantling(mantling);


			}
		}	
	}

	//for animation control
	public void mantleUp(){
		player.transform.position = endPosition;
		mantling = false;
		anim.SetBool(isMantleingHash, mantling);
		playerCont.setMantling(mantling);
		playerCont.checkCrouch();
	}
	
}

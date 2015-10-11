using UnityEngine;
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
		anim.SetBool(isMantleingHash, mantling);
		if (mantling){
			
			player.transform.position = mantlePosition;
			player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

			if(Input.GetKeyDown(KeyCode.W)){
				anim.SetTrigger(mantleUpHash);
			}
			
			if(Input.GetKeyDown(KeyCode.S)){
				//player.transform.position = mantleFall;
				mantling = false;
				playerCont.setMantling(mantling);
			}
		}
	}
	

	//while in the mantle area set the position and end position of the mantle
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "MantleCorner") {
			if(Input.GetKeyDown(KeyCode.W)){
				Debug.Log("should mantle");
				mantlePosition = other.transform.position;
				Transform child = other.transform.GetChild(0);
				Transform child2 = other.transform.GetChild(1);
				endPosition = child.transform.position;
				//mantleFall = child2.transform.position;

							
			}
			if (Input.GetKeyUp(KeyCode.W)){
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
	}
	
}

using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {

	private Animator anim;
	private PlayerControl controlComponent;
	private DeathNode deathCheck;
	private AudioSource audioController;
	public AudioClip[] clips;
	private AudioClip currentClip;
	int runHash = Animator.StringToHash("Speed");
	int crouchHash = Animator.StringToHash("Crouched");
	int groundHash = Animator.StringToHash("Grounded");
	int grapHash = Animator.StringToHash("Grappling");
	int mantHash = Animator.StringToHash("Mantling");
	int leftHash = Animator.StringToHash("Left");
	int jumpHash = Animator.StringToHash("Jump");
	int deathHash = Animator.StringToHash("Death");
	int interHash = Animator.StringToHash("Interact");

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		controlComponent = GetComponent<PlayerControl>();
		deathCheck = GameObject.FindGameObjectWithTag("DeathNode").GetComponent<DeathNode>();
		audioController = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis("Horizontal");
		anim.SetFloat(runHash, move);
		anim.SetBool(groundHash, controlComponent.getGrounded());
		anim.SetBool(grapHash, controlComponent.getIsGrappling());
		anim.SetBool(crouchHash, controlComponent.getIsCrouched());
		anim.SetBool (leftHash, controlComponent.getLeft());
		anim.SetBool (mantHash, controlComponent.getMantle());

		if(Input.GetKeyDown(KeyCode.Z)){
			anim.SetTrigger(interHash);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetTrigger (jumpHash);
		}

		if(deathCheck.getDeath()){
			anim.SetTrigger(deathHash);
		}
	}

	void footFalls(){
		currentClip = clips[Random.Range(0,2)];
		audioController.clip = currentClip;
		audioController.Play ();

	}
}

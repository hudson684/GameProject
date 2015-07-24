using UnityEngine;
using System.Collections;

public class playerAnimator : MonoBehaviour {

	private Animator anim;
	private PlayerControl controlComponent;
	private DeathNode deathCheck;
	private AudioSource audioController;
	public AudioClip[] clips;
	private AudioClip currentClip;
	private int jumpHash = Animator.StringToHash("Jump");
	private int groundHash = Animator.StringToHash("Grounded");
	private int speedHash = Animator.StringToHash("Speed");
	private int crouchHash = Animator.StringToHash("Crouching");
	private int Grappling = Animator.StringToHash("Grappling");
	private int DeathHash = Animator.StringToHash("Dead");
	private int interactHash = Animator.StringToHash("Interact");
	private bool crouched = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		controlComponent = GetComponent<PlayerControl>();
		deathCheck = GameObject.FindGameObjectWithTag("DeathNode").GetComponent<DeathNode>();
		audioController = GetComponent<AudioSource>();

	}

	void Update(){
		float move = Mathf.Abs(Input.GetAxis("Horizontal"));

		if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetTrigger (jumpHash);
		}

		if(Input.GetKeyDown(KeyCode.Z)){
			anim.SetTrigger(interactHash);
		}


		anim.SetFloat(speedHash,move);
		anim.SetBool(groundHash, controlComponent.getGrounded());
		anim.SetBool(Grappling, controlComponent.getIsGrappling());
		anim.SetBool(crouchHash, controlComponent.getIsCrouched());
		anim.SetBool(DeathHash,deathCheck.getDeath());

	}

	void footFall(){

		currentClip = clips[Random.Range(0,2)];
		audioController.clip = currentClip;
		audioController.Play();
	}
}

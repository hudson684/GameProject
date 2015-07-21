using UnityEngine;
using System.Collections;

public class DroneAnimator : MonoBehaviour {

	private Animator anim;
	private int idleHash = Animator.StringToHash("Idle");
	private int speedHash = Animator.StringToHash("Speed");
	private int attackHash = Animator.StringToHash("Attack");

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool(idleHash, false);
	}
}

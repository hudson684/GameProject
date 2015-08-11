using UnityEngine;
using System.Collections;

public class MechanicsNode : MonoBehaviour {

	private bool canGrapple = true;
	private bool canZeroG = true;

	public void setGrapple(bool grapple){

		canGrapple = grapple;
	}

	public bool getGrapple(){

		return canGrapple;
	}

	public void setZeroG(bool zeroG){

		canZeroG = zeroG;
	}

	public bool getZeroG(){

		return canZeroG;
	}



}

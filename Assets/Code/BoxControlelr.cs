using UnityEngine;
using System.Collections;

public class BoxControlelr : MonoBehaviour {
	//used by other scripts to controll box states
	public void destroyBox(){
		Destroy(this.gameObject);
	}
}

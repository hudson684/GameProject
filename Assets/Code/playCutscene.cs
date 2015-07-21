using UnityEngine;
using System.Collections;

public class playCutscene : MonoBehaviour {

	public GameObject door;
	public ElevatorClip Elevator;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			door.SetActive(true);
			Elevator.triggerCutscene();
			other.GetComponent<PlayerControl>().enabled = false;
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.SetParent(Elevator.transform);
			other.transform.SetParent(Elevator.transform);
		}
	}
}

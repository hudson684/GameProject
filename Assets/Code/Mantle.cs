using UnityEngine;
using System.Collections;

public class Mantle : MonoBehaviour {


	private bool mantling;
	private PlayerControl playerCont;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
		playerCont = (PlayerControl) this.GetComponentInParent(typeof(PlayerControl));
	
	}
	
	// Update is called once per frame
	void Update () {
		if (mantling) {
			//player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			player.GetComponent<Rigidbody2D>().Sleep();
			if(Input.GetKeyDown(KeyCode.Space)){
				player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10));
				mantling = false;
				playerCont.setMantling(mantling);
				player.GetComponent<Rigidbody2D>().WakeUp();
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
	}
	

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "MantleCorner") {
			if(playerCont.getIsGrappling()){
				if(Input.GetKeyDown(KeyCode.F)){
					mantling = true;
					playerCont.setMantling(mantling);
				}

			} else {
				mantling = true;
				playerCont.setMantling(mantling);			
			}
		}

	}

}

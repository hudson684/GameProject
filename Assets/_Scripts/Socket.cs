using UnityEngine;
using System.Collections;

public class Socket : Key {

	public Collider2D refCore;
	private Transform refTransform;

	void Start(){
		refTransform = transform.FindChild("DummyCore");
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other == refCore){
			Unlock();
			refCore.GetComponent<Rigidbody2D>().Sleep();
			refCore.GetComponent<BoxCollider2D>().enabled = false;
			refCore.transform.position = refTransform.position;
			refCore.transform.rotation = refTransform.rotation;
			if(refCore.transform.parent.tag == "Player"){
				refCore.transform.SetParent(null);
			}
			refCore.transform.SetParent(transform);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, refCore.transform.position);
	}
}

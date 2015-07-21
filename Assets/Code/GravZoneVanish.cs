using UnityEngine;
using System.Collections;

public class GravZoneVanish : MonoBehaviour {

	private BoxCollider2D boxCol;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = null;
		boxCol = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class objSpawn : MonoBehaviour {

	public GameObject spawnObject;
	public float interval;
	public float startingTime= 0;

	private float remainingTime;

	// Use this for initialization
	void Start () {
		//start Timer
		remainingTime = startingTime;
		spawnObject.tag = "Object";
	}
	
	// Update is called once per frame
	void Update () {
		if(remainingTime > 0){
			remainingTime -= Time.deltaTime;
		}else{
			//Spawn Object
			Instantiate(spawnObject, this.transform.position, Quaternion.identity);
			//Refresh Timer
			remainingTime = interval;
		}
	}
}

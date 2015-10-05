using UnityEngine;
using System.Collections;

public class objSpawn : MonoBehaviour {

	private GameObject spawnObject;
	public GameObject[] spawnObjects;
	public float interval;
	public float startingTime= 0;

	private float remainingTime;

	// Use this for initialization
	void Start () {
		//start Timer
		remainingTime = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(remainingTime > 0){
			remainingTime -= Time.deltaTime;
		}else{
			//Spawn Object
			SpawnRandom();
			//Refresh Timer
			remainingTime = interval;
		}
	}

	void SpawnRandom(){
		int index = Random.Range(0,spawnObjects.Length);
		float torque = Random.Range (-10f,10f);
		spawnObject = spawnObjects[index];
		GameObject temp = Instantiate(spawnObject, this.transform.position, Quaternion.identity) as GameObject;
		temp.tag = "Object";
		temp.GetComponent<Rigidbody2D>().AddTorque(torque);
	}
}

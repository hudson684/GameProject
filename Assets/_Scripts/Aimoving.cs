using UnityEngine;
using System.Collections;

public class Aimoving : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public float walkSpeed = 2.0f;
	public float wallLeft = 0.0f;
	public float wallRight = 5.0f;
	
	float walkingDirection = 1.0f;
	Vector3 walkAmount;
	
	// Update is called once per frame
	void Update () {
		
		//walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
		
		//if (walkingDirection &gt; 0.0f &amp;&amp; transform.position.x &gt;= wallRight)
		//	walkingDirection = -1.0f;
		//else if (walkingDirection &lt; 0.0f &amp;&amp; transform.position.x &lt;= wallLeft)
		//	walkingDirection = 1.0f;
		
		//transform.Translate(walkAmount);
	}
}

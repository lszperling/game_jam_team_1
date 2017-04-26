using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : MonoBehaviour {

	public Vector2 direction = new Vector2(0, 1);
	public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector2 (transform.position.x * direction.x * speed * Time.deltaTime, transform.position.x * direction.x * speed * Time.deltaTime);
	}
}

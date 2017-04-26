using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour {

	public float lifetime = 1f;
	private float currentLifeTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentLifeTime += Time.deltaTime;
		if (currentLifeTime > lifetime) {
			Destroy (this.gameObject);
		}
	}
}

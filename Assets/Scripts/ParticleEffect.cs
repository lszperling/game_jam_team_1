using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Effects";	
		GetComponent<ParticleSystem>().enableEmission = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

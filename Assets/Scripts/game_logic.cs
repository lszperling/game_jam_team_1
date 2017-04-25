using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_logic : MonoBehaviour {

	public Button button1;
	public Button button2;

	// Use this for initialization
	void Start () {
		//button1 = GameObject.Find ("alt_1");
		//button2 = GameObject.Find ("alt_2");
		button1.onClick.AddListener(ButtonClicked);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonClicked(){
		button1.GetComponentInChildren<Text>().text = "test";
	}



}

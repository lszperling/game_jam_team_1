using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_logic : MonoBehaviour {

	public Button button1;
	public Button button2;
	public Slider TimerSlider;
	public Text RightText;
	public Text WrongText;

	// Use this for initialization
	void Start () {
		//button1 = GameObject.Find ("alt_1");
		//button2 = GameObject.Find ("alt_2");
		button1.onClick.AddListener(() => ButtonClicked(button1));
		button2.onClick.AddListener(() => ButtonClicked(button2));


		//set Question
		button1.GetComponentInChildren<Text>().text = "Blue";
		button2.GetComponentInChildren<Text>().text = "Red";

		RightText.GetComponent<Text> ().enabled = false;
		WrongText.GetComponent<Text> ().enabled = false;
	
	}

	void FixedUpdate(){
		TimerSlider.value -= 0.05f;
	}
	
	// Update is called once per frame
	void Update () {	
		
	}

	public void ButtonClicked(Button btn){



		if (btn.GetComponentInChildren<Text> ().text == "Blue") {
			TimerSlider.value += 1;
			RightText.GetComponent<Text> ().enabled = true;
			WrongText.GetComponent<Text> ().enabled = false;

		} else {
			RightText.GetComponent<Text> ().enabled = false;
			WrongText.GetComponent<Text> ().enabled = true;
		}

		//String answer = getAnswer()
		//if(button1 == answer){

		//}




		//get new question
		//button1.GetComponentInChildren<Text>().text = "Answer 1";
		//button2.GetComponentInChildren<Text>().text = "Answer 2";

	}
}
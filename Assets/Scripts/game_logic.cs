﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class game_logic : MonoBehaviour {

	public Button button1;
	public Button button2;
	public Slider TimerSlider;
	public Text RightText;
	public Text WrongText;
	public Text QuestionText;
	public Image QuestionImage;


	//private string Question= "Color of the sky?"; 
	//private string Answer_1 = "blue";
	//private string Answer_2 = "red";
	//private string RightAnswer = "blue";



	private const float TimeLimit = 0.5f;
	private float UIUpdateTimer = TimeLimit;
	private bool StartTimer = false;


	private Question CurrentQuestion;


	// Use this for initialization
	void Start () {

		///content.Instantiate ();

		CurrentQuestion = GetComponent<content> ().getQuestion();

		//button1 = GameObject.Find ("alt_1");
		//button2 = GameObject.Find ("alt_2");
		button1.onClick.AddListener(() => ButtonClicked(button1));
		button2.onClick.AddListener(() => ButtonClicked(button2));


		//set Question
		RightText.GetComponent<Text> ().enabled = false;
		WrongText.GetComponent<Text> ().enabled = false;

		loadQuestion ();	
	}

	void FixedUpdate(){
		TimerSlider.value -= 0.05f;
	}
	
	// Update is called once per frame
	void Update () {	

		if(StartTimer){
			UIUpdateTimer -= Time.deltaTime;
			if ( UIUpdateTimer < 0 )
			{
				RightText.GetComponent<Text> ().enabled = false;
				WrongText.GetComponent<Text> ().enabled = false;
				UIUpdateTimer = TimeLimit;
				StartTimer = false;
			}
		}

	}

	public void ButtonClicked(Button btn){
		if (btn.GetComponentInChildren<Text> ().text == CurrentQuestion.correct) {
			TimerSlider.value += 5;
			RightText.GetComponent<Text> ().enabled = true;
			WrongText.GetComponent<Text> ().enabled = false;
			StartTimer = true;





		} else {
			RightText.GetComponent<Text> ().enabled = false;
			WrongText.GetComponent<Text> ().enabled = true;
			StartTimer = true;
		}

		//get new question
		//button1.GetComponentInChildren<Text>().text = "Answer 1";
		//button2.GetComponentInChildren<Text>().text = "Answer 2";

		CurrentQuestion = GetComponent<content> ().getQuestion();
		loadQuestion ();

	}

	private void loadQuestion() {
		//set a question
		//QuestionText.GetComponentInChildren<Text>().text = Question;
		button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
		button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;

		Sprite temp = Resources.Load<Sprite>("Images/"+CurrentQuestion.imgID);

		Debug.Log (""+temp.name);
		QuestionImage.GetComponent<Image> ().sprite = temp;
	}
}
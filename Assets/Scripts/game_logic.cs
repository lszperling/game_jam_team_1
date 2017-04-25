using System.Collections;
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
	public Image QuestionImage;
	public Text ScoreHandler;

	private const float TimeLimit = 0.5f;
	private float UIUpdateTimer = TimeLimit;
	private bool StartTimer = false;
	private int score = 0;

	private Question CurrentQuestion;

	// Use this for initialization
	void Start () {
		CurrentQuestion = GetComponent<content> ().getQuestion();

		//listeners for buttons
		button1.onClick.AddListener(() => ButtonClicked(button1));
		button2.onClick.AddListener(() => ButtonClicked(button2));

		//Remove right wrong text
		RightText.GetComponent<Text> ().enabled = false;
		WrongText.GetComponent<Text> ().enabled = false;

		loadQuestion ();	
	}

	void FixedUpdate(){
		TimerSlider.value -= 0.1f;

		ScoreHandler.GetComponent<Text> ().text = "SCORE: " + score;

		if (TimerSlider.GetComponent<Slider>().value <= 0) {

			//game over!
		}
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
			TimerSlider.value += 2;
			RightText.GetComponent<Text> ().enabled = true;
			WrongText.GetComponent<Text> ().enabled = false;
			StartTimer = true;

			score += 1000;

		} else {
			RightText.GetComponent<Text> ().enabled = false;
			WrongText.GetComponent<Text> ().enabled = true;
			StartTimer = true;
			TimerSlider.value -= 4;
		}

		//get new question
		CurrentQuestion = GetComponent<content> ().getQuestion();
		loadQuestion ();

	}

	private void loadQuestion() {
		//set a question
		//QuestionText.GetComponentInChildren<Text>().text = Question;

		int randomIndex = Random.Range (1, 3);

		if (randomIndex == 1) {
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}
		else if (randomIndex == 2) {
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}
		else{
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}

		Sprite temp = Resources.Load<Sprite>("Images/"+CurrentQuestion.imgID);

		Debug.Log (""+temp.name);
		QuestionImage.GetComponent<Image> ().sprite = temp;
	}
}
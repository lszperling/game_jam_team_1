using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class game_logic : MonoBehaviour {

	public Button button1;
	public Button button2;
	public Image TimerSlider;
	public Image TimerAddTimeOverlay;
	public Text RightText;
	public Text WrongText;
	public Image QuestionImage;
	public Text ScoreHandler;
	public Button PauseButton;
	public GameObject PlayArea;
	public GameObject GameOver;
	public Text FinishScore;
	public Text HighScore;
	public Image TransitionImage;
	public Text scoreFlyer;
	public Text titleLevel;
	public Text levelUpFlyer;
	public Text timeUpFlyer;

	private string highscoreKey = "highscore";
	private const float TimeLimit = 0.5f;
	private float UIUpdateTimer = TimeLimit;
	private bool StartTimer = false;
	//private bool Paused = false;

	private int TransWidth;
	private int TransHeight;

	private Question CurrentQuestion;

	private ScoreKeeper ScoreKeep;

	private string currentTitle;

	// Use this for initialization
	void Start () {
		CurrentQuestion = GetComponent<content> ().getQuestion();
		ScoreKeep = GetComponent<ScoreKeeper> ();
		currentTitle = ScoreKeep.currentTitle ();
		titleLevel.GetComponent<Text> ().text = currentTitle;

		//listeners for buttons
		button1.onClick.AddListener(() => ButtonClicked(button1));
		button2.onClick.AddListener(() => ButtonClicked(button2));

		//Remove right wrong text
		RightText.GetComponent<Text> ().enabled = false;
		WrongText.GetComponent<Text> ().enabled = false;

		loadQuestion ();	

		TransWidth = 0;
		TransHeight = 0;

	}

	void FixedUpdate(){
		float minusTime = -0.1f/100;
		changeTimersFillValues (minusTime);

		if (TimerSlider.fillAmount < 0.2f) {
			TimerSlider.GetComponent<Animator> ().SetTrigger ("pulsate");
		} else {
			TimerSlider.GetComponent<Animator> ().SetTrigger ("default");
		}
	
		ScoreHandler.GetComponent<Text> ().text = "" + ScoreKeep.currentScore;

		if (TimerSlider.GetComponent<Image>().fillAmount <= 0) {


			RectTransform TransRectTrans = TransitionImage.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {
				//SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 
				PlayArea.SetActive (false);
				GameOver.SetActive (true);
				FinishScore.GetComponent<Text> ().text = "" + ScoreKeep.currentScore;


				int current_score = (int)ScoreKeep.currentScore;

				int highscore = PlayerPrefs.GetInt (highscoreKey);
				if (current_score > highscore) {
					PlayerPrefs.SetInt (highscoreKey, current_score);
				}

				highscore = PlayerPrefs.GetInt (highscoreKey);
				HighScore.GetComponent<Text> ().text = "" + highscore.ToString ();

			} else {
				TransHeight += 100;
				TransWidth += 100;
				TransRectTrans.sizeDelta = new Vector2( TransWidth, TransHeight);
			}

			//Text final_thing;
			//final_thing = GameObject.Find("final_score") as Text;
			//final_thing.text = Text "hello";
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

			changeTimersFillValues (0.02f);
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseGreen");

			timeUpFlyer.transform.position = positionForTimeUpFlyer ();
		
			StartTimer = true;

			float gotScore = ScoreKeep.answerCorrect ();
			scoreFlyer.GetComponent<Text> ().text = "+" + gotScore;
			scoreFlyer.GetComponent<Animator> ().SetTrigger ("fly");

			btn.GetComponent<Animator> ().SetTrigger ("altBtnCorrect");


			Debug.Log ("CURRENT TITLE:" + ScoreKeep.currentTitle());
			if (ScoreKeep.currentTitle () != currentTitle) {
				//Level up!

				currentTitle = ScoreKeep.currentTitle ();
				titleLevel.GetComponent<Text> ().text = currentTitle;
				levelUpFlyer.GetComponent<Text> ().text = "Level up!";
				levelUpFlyer.GetComponent<Animator> ().SetTrigger ("show");
			}

		} else {
			StartTimer = true;
			changeTimersFillValues (-0.06f);
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseRed");
			ScoreKeep.answerWrong ();
			btn.GetComponent<Animator> ().SetTrigger ("altBtnWrong");
		}

		//get new question
		CurrentQuestion = GetComponent<content> ().getQuestion();
		loadQuestion ();

	}


	private Vector2 positionForTimeUpFlyer(){


		float angleDegrees = (360 * TimerSlider.fillAmount) + 90;
		Debug.Log ("Degrees:" + angleDegrees);

		float radians = angleDegrees * Mathf.Deg2Rad;

		var x = Mathf.Cos(radians);
		var y = Mathf.Sin(radians);
		Vector2 center = TimerSlider.transform.position;
		Vector2 position = center + new Vector2 (x, y);
		return position * 3;


	}

	private void loadQuestion() {
		int randomIndex = Random.Range (1, 3);

		button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
		button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;

//		if (randomIndex == 1) {
//			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
//			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
//		}
//		else if (randomIndex == 2) {
//			button2.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
//			button1.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
//		}
//		else{
//			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
//			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
//		}
		Sprite temp = Resources.Load<Sprite>("Images/"+CurrentQuestion.imgID);
		QuestionImage.GetComponent<Image> ().sprite = temp;
	}

	private void changeTimersFillValues(float deltaValue) {
		TimerSlider.fillAmount += deltaValue;
		TimerAddTimeOverlay.fillAmount += deltaValue;
	}

}
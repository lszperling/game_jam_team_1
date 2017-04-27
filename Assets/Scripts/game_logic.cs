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
	public Button rateButton;
	public AudioSource MusicSource;


	private string highscoreKey = "highscore";
	private const float TimeLimit = 0.5f;
	private float UIUpdateTimer = TimeLimit;
	private bool StartTimer = false;
	//private bool Paused = false;
	private float currentMultiplier = 1;

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

		rateButton.onClick.AddListener(() => LeaveRating());
		//Remove right wrong text
		RightText.GetComponent<Text> ().enabled = false;
		WrongText.GetComponent<Text> ().enabled = false;

		loadQuestion ();	

		TransWidth = 0;
		TransHeight = 0;

	}

	void FixedUpdate(){

		ScoreHandler.GetComponent<Text> ().text = "" + ScoreKeep.currentScore;

		if (PlayArea.activeSelf) {
			if (!MusicSource.isPlaying){
				MusicSource.Play ();
			}
			float minusTime = (-0.1f * (0f + (ScoreKeep.currentLevel ()))) / 100;
			changeTimersFillValues (minusTime);

			currentMultiplier = ScoreKeep.currentMultiplier ();

			if (TimerSlider.fillAmount < 0.4f) {
				TimerSlider.GetComponent<Animator> ().SetTrigger ("pulsate");
			} else {
				TimerSlider.GetComponent<Animator> ().SetTrigger ("default");
			}

			//Set pitch of music
			float currentLevel = -1f + (float)ScoreKeep.currentLevel ();
			MusicSource.GetComponent<AudioSource> ().pitch = 1f + (currentLevel / 10);

			if (TimerSlider.GetComponent<Image> ().fillAmount <= 0) {

				MusicSource.Stop ();
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
					TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);
				}

				//Text final_thing;
				//final_thing = GameObject.Find("final_score") as Text;
				//final_thing.text = Text "hello";
			}
		} else {
			MusicSource.Stop ();
		}
	}
	
	// Update is called once per frame
	void Update () {	
		if (PlayArea.activeSelf) {
			if (StartTimer) {
				UIUpdateTimer -= Time.deltaTime;
				if (UIUpdateTimer < 0) {
					RightText.GetComponent<Text> ().enabled = false;
					WrongText.GetComponent<Text> ().enabled = false;
					UIUpdateTimer = TimeLimit;
					StartTimer = false;
				}
			}
		}
	}

	public void ButtonClicked(Button btn){
		btn.GetComponent<AudioSource> ().Play ();

		if (btn.GetComponentInChildren<Text> ().text == CurrentQuestion.correct) {
<<<<<<< Updated upstream
			changeTimersFillValues (0.02f * (1 + ScoreKeep.currentMultiplier()) / 2);
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseGreen");
=======
			changeTimersFillValues (0.02f);

			Debug.Log ("CurrentMultipler" + currentMultiplier);
			Debug.Log ("scoreKeep.currentMultiplier" + ScoreKeep.currentMultiplier());





>>>>>>> Stashed changes

			timeUpFlyer.transform.position = positionForTimeUpFlyer ();

			StartTimer = true;

			float gotScore = ScoreKeep.answerCorrect ();
			ScoreHandler.GetComponent<Animator> ().SetTrigger ("bounce");
			scoreFlyer.GetComponent<Text> ().text = "+" + gotScore;
			scoreFlyer.GetComponent<Animator> ().SetTrigger ("fly");

			btn.GetComponent<Animator> ().SetTrigger ("default");
			btn.GetComponent<Animator> ().SetTrigger ("altBtnCorrect");

			if (currentMultiplier != ScoreKeep.currentMultiplier ()) {
				//STREEAK!
				Debug.Log ("STREAAAK");
				TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("streakMode");
			} else {
				TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseGreen");
			}

			currentMultiplier = ScoreKeep.currentMultiplier ();

			if (ScoreKeep.currentTitle () != currentTitle) {
				//Level up!
				setTimersFillValues(1);
				currentTitle = ScoreKeep.currentTitle ();
				titleLevel.GetComponent<Text> ().text = currentTitle;
				levelUpFlyer.GetComponent<Text> ().text = "Level up!";
				levelUpFlyer.GetComponent<Animator> ().SetTrigger ("show");
			}




		} else {
			StartTimer = true;
			changeTimersFillValues (-0.06f);
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("default");
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseRed");
			ScoreKeep.answerWrong ();


			btn.GetComponent<Animator> ().SetTrigger ("default");
			btn.GetComponent<Animator> ().SetTrigger ("altBtnWrong");
		}

		//get new question
		CurrentQuestion = GetComponent<content> ().getQuestion();
		loadQuestion ();
	}

	private Vector2 positionForTimeUpFlyer(){

		float angleDegrees = (360 * TimerSlider.fillAmount) + 90;
		//Debug.Log ("Degrees:" + angleDegrees);

		float radians = angleDegrees * Mathf.Deg2Rad;

		var x = Mathf.Cos(radians);
		var y = Mathf.Sin(radians);
		Vector2 position = new Vector2 (x, y -5);
		return position * 3;
	}

	private void loadQuestion() {
		int randomIndex = Random.Range (1, 3);
<<<<<<< Updated upstream

		//button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
		//button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;

=======
	
>>>>>>> Stashed changes
		if (randomIndex == 1) {
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}
<<<<<<< Updated upstream
		else if (randomIndex == 2) {
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}
		else{
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}
=======
		else {
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}

>>>>>>> Stashed changes
		Sprite temp = Resources.Load<Sprite>("Images/"+CurrentQuestion.imgID);
		QuestionImage.GetComponent<Image> ().sprite = temp;
	}

	private void changeTimersFillValues(float deltaValue) {
		TimerSlider.fillAmount += deltaValue;
		TimerAddTimeOverlay.fillAmount += deltaValue;
	}

	private void setTimersFillValues(float value){
		TimerSlider.fillAmount = value;
		TimerAddTimeOverlay.fillAmount = value;
	}
		
	private void LeaveRating(){
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.BlipBlop.Tugodumka");
	}

}
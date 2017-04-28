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
	public ParticleSystem starsParticleSystem;
	public Text levelScore;


	private string highscoreKey = "highscore";
	private const float TimeLimit = 0.5f;
	private float UIUpdateTimer = TimeLimit;
	private bool StartTimer = false;
	//private bool Paused = false;
	private float currentMultiplier = 1;
	private int currentStreakLevel = 0;

	private int TransWidth;
	private int TransHeight;

	private Question CurrentQuestion;

	private ScoreKeeper ScoreKeep;

	private string currentTitle;

	private bool isStreaking;

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

		levelUpFlyer.GetComponent<Text>().text = "Очки: " + ScoreKeep.currentMultiplier () + "x";

	}

	void FixedUpdate(){

	
		ScoreHandler.GetComponent<Text> ().text = "" + ScoreKeep.currentScore;

		if (isStreaking && !starsParticleSystem.isPlaying) {
			starsParticleSystem.Play ();
		}
					
		if (PlayArea.activeSelf) {
			if (!MusicSource.isPlaying){
				MusicSource.Play ();
			}
			float minusTime = (-0.1f * (0f + (ScoreKeep.currentLevel ()))) / 140;
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
					levelScore.text = "" + ScoreKeep.currentTitle();
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

			changeTimersFillValues (0.02f * (1 + ScoreKeep.currentMultiplier()) / 2);

			timeUpFlyer.transform.position = positionForTimeUpFlyer ();

			StartTimer = true;

			float gotScore = ScoreKeep.answerCorrect ();
			ScoreHandler.GetComponent<Animator> ().SetTrigger ("bounce");
			scoreFlyer.GetComponent<Text> ().text = "+" + gotScore;

			if (currentStreakLevel == 1) {
				scoreFlyer.GetComponent<Animator> ().SetTrigger ("fly1");
			} else if (currentStreakLevel == 2) {
				scoreFlyer.GetComponent<Animator> ().SetTrigger ("fly2");
			}  else if (currentStreakLevel == 3){
				scoreFlyer.GetComponent<Animator> ().SetTrigger ("fly3");
			}else {
				scoreFlyer.GetComponent<Animator> ().SetTrigger ("fly0");
			}
				
			btn.GetComponent<Animator> ().SetTrigger ("default");
			btn.GetComponent<Animator> ().SetTrigger ("altBtnCorrect");

			if (ScoreKeep.currentMultiplier () > currentMultiplier) {
				levelUpFlyer.GetComponent<Text>().text = "Бонусные очки: " + "x" + ScoreKeep.currentMultiplier ();
				levelUpFlyer.GetComponent<Animator> ().SetTrigger ("streakon");

				currentStreakLevel += 1;
				streakMode (true, currentStreakLevel);
			}

			currentMultiplier = ScoreKeep.currentMultiplier ();

			if (!isStreaking) {
				TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseGreen");
			}

			if (ScoreKeep.currentTitle () != currentTitle) {
				//Level up!
				ScoreKeep.currentStreak = 0;

				streakMode (false, 1);
				setTimersFillValues(1);

				currentTitle = ScoreKeep.currentTitle ();
				titleLevel.GetComponent<Text> ().text = currentTitle;
			}
				
		} else {
			StartTimer = true;
			changeTimersFillValues (-0.06f);
			currentStreakLevel = 0;

			if (!isStreaking) {
				TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("singlePulseRed");
			}

			streakMode (false, currentStreakLevel);

			ScoreKeep.answerWrong ();

			btn.GetComponent<Animator> ().SetTrigger ("default");
			btn.GetComponent<Animator> ().SetTrigger ("altBtnWrong");

			levelUpFlyer.GetComponent<Text>().text = "Бонусные очки: " + "x" + ScoreKeep.currentMultiplier ();
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

		if (randomIndex == 1) {
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
		}

		else{
			button1.GetComponentInChildren<Text>().text = CurrentQuestion.incorrect;
			button2.GetComponentInChildren<Text>().text = CurrentQuestion.correct;
		}


		string imagePath = "Images_animals/" + CurrentQuestion.imgID;

		switch (PlayerPrefs.GetInt ("selectedTopic")) {
		case 1:
			imagePath = "Images_sweetshop/" + CurrentQuestion.imgID;
			break;
		case 2:
			imagePath = "Images_geo/" + CurrentQuestion.imgID;
			break;
		case 3:
			imagePath = "Images_animals/" + CurrentQuestion.imgID;
			break;
		case 4:
			imagePath = "Images/" + CurrentQuestion.imgID;
			break;
		case 5:
			imagePath = "Images_random/" + CurrentQuestion.imgID;
			break;
		}

		Sprite temp = Resources.Load<Sprite>(imagePath);
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


	private void streakMode(bool streakModeOn, int streakLevel) {
		isStreaking = streakModeOn;
		Debug.Log ("streak:" + streakModeOn);
		if (streakModeOn == true) {
			Debug.Log ("STREAK ON");
			Color temp = TimerAddTimeOverlay.color;
			temp.a= 1f;
			TimerAddTimeOverlay.color = temp;
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("streakMode");
			UnityEngine.ParticleSystem.EmissionModule em = starsParticleSystem.emission;
			em.rateOverTime = 50f * (currentStreakLevel + 1);
			//starsParticleSystem.emission.SetBursts (1000);
			starsParticleSystem.Play ();	
		} else {
			TimerAddTimeOverlay.GetComponent<Animator> ().SetTrigger ("default");
			Color temp = TimerAddTimeOverlay.color;
			temp.a=0f;
			TimerAddTimeOverlay.color = temp;
			Debug.Log ("STREAK OFF");
			starsParticleSystem.Stop ();
		}

	}



}
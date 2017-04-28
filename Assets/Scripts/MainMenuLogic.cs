using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuLogic : MonoBehaviour {


	public Button StartGameButton;
	public Button StartGameButtonTopic2;
	public Button StartGameButtonTopic3;
	public Button StartGameButtonTopic4;
	public Button StartGameButtonTopic5;

	public Text TimeLeftUnlock;
	public Text TimeLeftUnlock3;
	public Text TimeLeftUnlock4;
	public Text TimeLeftUnlock5;

	public GameObject LockImage;
	public GameObject LockImage2;
	public GameObject LockImage3;
	public GameObject LockImage4;

	public Button RulesButton;
	public Button CreditsButton;
	public Text HighScore;
	public GameObject TransImageLeft;
	public GameObject TransImageRight;

	private int highscore = 0;
	private bool StartGameClicked = false;
	private bool ShowRulesClicked =  false;
	private bool CreditsRulesClicked =  false;
	private int TransWidth;
	private int TransHeight;
	private string installDateKey = "InstallDate";


	// Use this for initialization
	void Start () {

		StartGameButton.GetComponent<Animator> ().SetTrigger ("breathe");


		if (!PlayerPrefs.HasKey (installDateKey)) {
			PlayerPrefs.SetString (installDateKey, System.DateTime.Now.ToString());
		}

		//StartGameButton.onClick.AddListener(() => StartGame());

		StartGameButton.onClick.AddListener(() => StartGame(1));

		if (Topic2Enabled()) {
			StartGameButtonTopic2.onClick.AddListener(() => StartGame(2));
			StartGameButtonTopic2.GetComponent<Animator> ().SetTrigger ("breathe");
		}
		if (Topic3Enabled()) {
			StartGameButtonTopic3.onClick.AddListener(() => StartGame(3));
			StartGameButtonTopic3.GetComponent<Animator> ().SetTrigger ("breathe");
		}
		if (Topic4Enabled()) {
			StartGameButtonTopic4.onClick.AddListener(() => StartGame(4));
			StartGameButtonTopic4.GetComponent<Animator> ().SetTrigger ("breathe");
		}
		if (Topic5Enabled()) {
			StartGameButtonTopic5.onClick.AddListener(() => StartGame(5));
			StartGameButtonTopic5.GetComponent<Animator> ().SetTrigger ("breathe");
		}


		RulesButton.onClick.AddListener(() => ShowRules());
		CreditsButton.onClick.AddListener(() => ShowCredits());

		if (PlayerPrefs.HasKey ("highscore")) {
			HighScore.GetComponent<Text> ().text = "High score: " + PlayerPrefs.GetInt ("highscore").ToString();
			HighScore.gameObject.SetActive(true);
		}

		//set the highscore text
		highscore = PlayerPrefs.GetInt ("highscore");
		HighScore.GetComponent<Text> ().text = ""+highscore;

		TransWidth = 0;
		TransHeight = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (StartGameClicked) {
		
			RectTransform TransRectTrans = TransImageLeft.GetComponent<RectTransform> ();
		
			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {	
				SceneManager.LoadScene ("Game", LoadSceneMode.Single); 			
			} else {			
				TransHeight += 100;
				TransWidth += 100;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);			
			}		
		} 
		else if (ShowRulesClicked) {

			RectTransform TransRectTrans = TransImageLeft.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {
				SceneManager.LoadScene ("RulesMenu", LoadSceneMode.Single); 
			} else {
				TransHeight += 100;
				TransWidth += 100;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);
			}		
		}
		else if (CreditsRulesClicked) {
			RectTransform TransRectTrans = TransImageRight.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {
				SceneManager.LoadScene ("CreditsMenu", LoadSceneMode.Single); 
			} else {
				TransHeight += 100;
				TransWidth += 100;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);
			}		
		}

		TimeSpan timeDifference = GetTimeFromFinalDate ();

		if (!Topic2Enabled()) {
			TimeLeftUnlock.GetComponent<Text> ().text = CountdownString(timeDifference);
			LockImage.SetActive (true);
			TimeLeftUnlock.GetComponent<Text> ().fontSize = 30;
		}

		if (!Topic3Enabled()) {
			TimeLeftUnlock3.GetComponent<Text> ().text = CountdownString(timeDifference);
			LockImage2.SetActive (true);
		}

		if (!Topic4Enabled()) {
			TimeLeftUnlock4.GetComponent<Text> ().text = CountdownString(timeDifference);
			LockImage3.SetActive (true);
		}

		if (!Topic5Enabled()) {
			TimeLeftUnlock5.GetComponent<Text> ().text = CountdownString(timeDifference);
			LockImage4.SetActive (true);
		}

		switch (GetTimeFromFinalDate ().Days) 
		{
		case 4:
			StartGameButtonTopic3.gameObject.SetActive (false);
			StartGameButtonTopic4.gameObject.SetActive (false);
			StartGameButtonTopic5.gameObject.SetActive (false);
			break;

		case 3:
			StartGameButtonTopic4.gameObject.SetActive (false);
			StartGameButtonTopic5.gameObject.SetActive (false);
			break;

		case 2:
			StartGameButtonTopic5.gameObject.SetActive (false);
			break;

		}
	}

	private void StartGame(int topic){
		if (!ShowRulesClicked && !CreditsRulesClicked) {
			PlayerPrefs.SetInt("selectedTopic",topic);
			StartGameClicked = true;
		}
	}
	private void ShowRules(){
		if (!StartGameClicked && !CreditsRulesClicked) {
			ShowRulesClicked = true;
		}
	}
	private void ShowCredits(){
		if (!StartGameClicked && !ShowRulesClicked) {
			CreditsRulesClicked = true;
		}
	}

	private bool Topic2Enabled(){
		return GetTimeFromFinalDate ().Days < 4;
	}

	private bool Topic3Enabled(){
		return GetTimeFromFinalDate ().Days < 3;
	}

	private bool Topic4Enabled(){
		return GetTimeFromFinalDate ().Days < 2;
	}

	private bool Topic5Enabled(){
		return GetTimeFromFinalDate ().Days < 1;
	}

	private String CountdownString(TimeSpan timeDifference){
		return MakeDoubleDigit(timeDifference.Hours) + ":" + 
			MakeDoubleDigit(timeDifference.Minutes) + ":" + 
			MakeDoubleDigit(timeDifference.Seconds);
	}

	private String MakeDoubleDigit(int number){
		if (number < 10) {
			return "0" + number;
		}
		return number.ToString ();
	}

	private TimeSpan GetTimeFromFinalDate(){
		DateTime installDate = Convert.ToDateTime (PlayerPrefs.GetString (installDateKey).ToString ());
		return installDate.AddDays(5) - System.DateTime.Now;
	}
}
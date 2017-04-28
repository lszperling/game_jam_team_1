using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ContentMenuLogic : MonoBehaviour {


	public Button StartGameButton;
	public Button StartGameButtonTopic2;
	public Button StartGameButtonTopic3;
	public Button StartGameButtonTopic4;
	public Button StartGameButtonTopic5;

	public Button RulesButton;
	public Text TimeLeft;
	public Text TimeLeftUnlock;
	public Text TimeLeftUnlock3;
	public Text TimeLeftUnlock4;
	public Text TimeLeftUnlock5;

	public GameObject TransImage;
	public GameObject TransImage3;
	public GameObject TransImage4;
	public GameObject TransImage5;


	private string installDateKey = "InstallDate";
	private bool StartGameClicked = false;
	private bool ShowRulesClicked =  false;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey (installDateKey)) {
			PlayerPrefs.SetString (installDateKey, System.DateTime.Now.ToString());
		}

		//RulesButton.onClick.AddListener(() => ShowRules());
		//StartGameButton.onClick.AddListener(() => StartGame());

		StartTopics ();
		TimeLeft.GetComponent<Text> ().text = PlayerPrefs.GetString (installDateKey).ToString();
		TimeLeft.gameObject.SetActive(true);


	}

	// Update is called once per frame
	void Update () {

		TimeSpan timeDifference = GetTimeFromFinalDate ();

		if (!Topic2Enabled()) {
			TimeLeftUnlock.GetComponent<Text> ().text = timeDifference.Hours + ":" + 
				timeDifference.Minutes + ":" + timeDifference.Seconds;
			TransImage.SetActive (true);
			TimeLeftUnlock.GetComponent<Text> ().fontSize = 30;
			//TimeLeftUnlock.GetComponent<Text> ().fon
		}

		if (!Topic3Enabled()) {
			TimeLeftUnlock3.GetComponent<Text> ().text = timeDifference.Hours + ":" + 
				timeDifference.Minutes + ":" + timeDifference.Seconds;
			TransImage3.SetActive (true);
		}

		if (!Topic4Enabled()) {
			TimeLeftUnlock4.GetComponent<Text> ().text = timeDifference.Hours + ":" + 
				timeDifference.Minutes + ":" + timeDifference.Seconds;
			TransImage4.SetActive (true);
		}

		if (!Topic5Enabled()) {
			TimeLeftUnlock5.GetComponent<Text> ().text = timeDifference.Hours + ":" + 
				timeDifference.Minutes + ":" + timeDifference.Seconds;
			TransImage5.SetActive (true);
		}
		//RulesButton.gameObject.SetActive (false);


		Debug.Log (GetTimeFromFinalDate ().Days);
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

	private void StartGame(){
		if (!ShowRulesClicked) {
			StartGameClicked = true;
		}
	}
	private void ShowRules(){
		if (!StartGameClicked) {
			ShowRulesClicked = true;
		}
	}


	private void StartTopics(){
		if (Topic2Enabled()) {
			//StartGameButtonTopic2.onClick.AddListener(() => StartGame());
		}
		if (Topic3Enabled()) {
			//StartGameButtonTopic3.onClick.AddListener(() => StartGame());
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

	private void DiplayCountdownButton(int buttonNumber){


		/*TimeSpan timeDifference = GetTimeFromFinalDate ();



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




		if (!Topic2Enabled()) {
			TimeLeftUnlock.GetComponent<Text> ().text = timeDifference.Hours + ":" + 
				timeDifference.Minutes + ":" + timeDifference.Seconds;
			TransImage.SetActive (true);
			TimeLeftUnlock.GetComponent<Text> ().fontSize = 30;
		}*/
	}

	private TimeSpan GetTimeFromFinalDate(){
		DateTime installDate = Convert.ToDateTime (PlayerPrefs.GetString (installDateKey).ToString ());
		return installDate.AddDays(5) - System.DateTime.Now;
	}
}
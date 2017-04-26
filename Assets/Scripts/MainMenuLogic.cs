using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {


	public Button StartGameButton;
	public Button RulesButton;
	public Text HighScore;


	// Use this for initialization
	void Start () {

		StartGameButton.onClick.AddListener(() => StartGame());
		RulesButton.onClick.AddListener(() => ShowRules());

		if (PlayerPrefs.HasKey ("highscore")) {
			HighScore.GetComponent<Text> ().text = "High score: " + PlayerPrefs.GetInt ("highscore").ToString();
			HighScore.gameObject.active = true;
		}


		//button2.onClick.AddListener(() => ButtonClicked(button2));
	}
	
	// Update is called once per frame
	void Update () {



	}

	private void StartGame(){
	
		SceneManager.LoadScene("Game", LoadSceneMode.Single); 

	}
	private void ShowRules(){

		SceneManager.LoadScene("RulesMenu", LoadSceneMode.Single); 

	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {


	public Button StartGameButton;
	public Button RulesButton;
	public Text HighScore;
	public Image TransImage;

	private int highscore = 0;
	private bool StartGameClicked = false;
	private bool ShowRulesClicked =  false;
	private int TransWidth;
	private int TransHeight;


	// Use this for initialization
	void Start () {

		StartGameButton.onClick.AddListener(() => StartGame());
		RulesButton.onClick.AddListener(() => ShowRules());

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
		
			RectTransform TransRectTrans = TransImage.GetComponent<RectTransform> ();
		
			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {	
				SceneManager.LoadScene ("Game", LoadSceneMode.Single); 			
			} else {			
				TransHeight += 100;
				TransWidth += 100;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);			
			}		
		} 
		else if (ShowRulesClicked) {

			RectTransform TransRectTrans = TransImage.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {
				SceneManager.LoadScene ("RulesMenu", LoadSceneMode.Single); 
			} else {
				TransHeight += 100;
				TransWidth += 100;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);
			}		
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
}
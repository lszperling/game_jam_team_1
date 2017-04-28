using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreenLogic : MonoBehaviour {

	public Button StartButton;
	public Text Title;
	public Text Score;
	public GameObject PlayArea;
	public GameObject LevelUpScreen;

	private ScoreKeeper Keeper;
	private string currentTitle;

	// Use this for initialization
	void Start () {
		StartButton.onClick.AddListener(() => StartGame());
		Keeper = GetComponentInParent<ScoreKeeper> ();
	}
	
	// Update is called once per frame
	void Update () {	
	
		Title.GetComponent<Text> ().text = Keeper.currentTitle();
		Score.GetComponent<Text> ().text = ""+Keeper.currentScore;

		if (LevelUpScreen.activeSelf) {
			//Title.GetComponent<Text> ().text = Keeper.currentTitle();
			//Score.GetComponent<Text> ().text = ""+Keeper.currentScore;
		}
		if (Keeper.currentTitle () != currentTitle) {
			Keeper.currentStreak = 0;
			PlayArea.SetActive (false);
			LevelUpScreen.SetActive (true);
		}

		currentTitle = Keeper.currentTitle();
	}
	public void StartGame(){
		PlayArea.SetActive (true);
		LevelUpScreen.SetActive (false);
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	// Use this for initialization
	public float currentScore = 0;
	public int currentStreak = 0;

	public int comboBreak1 = 3;
	public float comboMultiplier1 = 2f;

	public int comboBreak2 = 6;
	public float comboMultiplier2 = 3f;

	//public List<string> titles = ['Тугодум', 'Тормоз', 'Слоупок', 'Черепаха',  'Живчик', 'Пострел', 'Ветер', 'Молния', 'Пуля', 'Шустряк'];
	public List<string> titles;

	public List<float> titleScoreLevels;

	public float timePenaltyFactor = 0.1f;

	public float maxScorePerQuestion = 1000f;
	float scorePenaltyPerSecond;

	void Start () {
		scorePenaltyPerSecond = maxScorePerQuestion * timePenaltyFactor;
	}


	public float currentMultiplier(){
		float comboMultiplier = 1;

		if (currentStreak >= comboBreak1) {
			comboMultiplier = comboMultiplier1;
		}

		if (currentStreak >= comboBreak2) {

			comboMultiplier = comboMultiplier2;
		}

		return comboMultiplier;
	}

	public float answerCorrect(float answerTime = 0){

		float score = maxScorePerQuestion - (answerTime * scorePenaltyPerSecond);

		score = score * currentMultiplier ();



		currentScore += score;
		currentStreak += 1;

		return score;
	}

	public void answerWrong(){
		currentStreak = 0;
	}



	public string currentTitle(){
		int i = 0;
		string title = "";
		foreach(float scoreLevel in titleScoreLevels) {
			if (currentScore >= scoreLevel) {
				title = titles [i];
			}
			i += 1;
		}
		return title;
	}

	// Update is called once per frame
	void Update () {

	}
}
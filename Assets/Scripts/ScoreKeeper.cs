using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	// Use this for initialization

	public int currentScore = 0;
	public int currentStreak = 0;

	public int comboBreak1 = 3;
	public float comboMultiplier1 = 2f;

	public int comboBreak2 = 6;
	public float comboMultiplier2 = 3f;

	public float timePenaltyFactor = 0.1f;

	public float maxScorePerQuestion = 1000f;
	float scorePenaltyPerSecond;

	void Start () {
		scorePenaltyPerSecond = maxScorePerQuestion * timePenaltyFactor;
	}


	public int currentMultiplier(){
		float comboMultiplier;
		if (currentStreak >= comboBreak1) {
			comboMultiplier1 = comboMultiplier1;
		} else if (currentStreak >= comboBreak2) {
			comboMultiplier = comboMultiplier2;
		}

		return comboMultiplier;
	}

	public float answerCorrect(float answerTime = 0){

		float score = maxScorePerQuestion - (answerTime * scorePenaltyPerSecond);

		if(currentScore >= comboBreak1 ) {
			score = score * comboMultiplier1;
		}
		else if (currentScore >= comboBreak2) {
			score = score * comboMultiplier2;
		}

		currentScore += score;
		currentStreak += 1;

		return score;
	}

	public void answerWrong(){
		currentStreak = 0;
	}


	// Update is called once per frame
	void Update () {

	}
}

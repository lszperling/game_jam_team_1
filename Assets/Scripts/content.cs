using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class Question {
	public string correct;
	public string incorrect;
	public string imgID;
}
[System.Serializable]
public class QuestionList {
	public List<Question> questions;
}


public class content : MonoBehaviour {

	public QuestionList questionList;
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}
		
	private void LoadQuestions(){
		TextAsset asset = Resources.Load (Path.Combine ("Questions", "questions")) as TextAsset;

		if (PlayerPrefs.GetInt ("selectedTopic") == 2) {
			asset = Resources.Load (Path.Combine ("Questions", "questions_animals")) as TextAsset;
		} 
		if (PlayerPrefs.GetInt ("selectedTopic") == 3) {
			asset = Resources.Load (Path.Combine ("Questions", "questions_geo")) as TextAsset;
		} 
		if (PlayerPrefs.GetInt ("selectedTopic") == 4) {
			asset = Resources.Load (Path.Combine ("Questions", "questions_sweetshop")) as TextAsset;
		} 
		if (PlayerPrefs.GetInt ("selectedTopic") == 5) {
			asset = Resources.Load (Path.Combine ("Questions", "questions_random")) as TextAsset;
		} 


		questionList = JsonUtility.FromJson<QuestionList> (asset.text);
	}

	public Question getQuestion() {

		if (questionList.questions.Count == 0) {
			LoadQuestions ();
		}
			
		int randomIndex = Random.Range (0, questionList.questions.Count);
		Question q = questionList.questions [randomIndex];
		questionList.questions.RemoveAt (randomIndex);
		return q;
	}
}
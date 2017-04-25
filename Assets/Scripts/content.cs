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

	public Question getQuestion() {

		if (questionList.questions.Count == 0) {
			TextAsset asset = Resources.Load (Path.Combine ("Questions", "questions")) as TextAsset;
			questionList = JsonUtility.FromJson<QuestionList> (asset.text);
			Debug.Log ("apekatt");
			Debug.Log (questionList.questions [0].correct);
		}
			

		int randomIndex = Random.Range (0, questionList.questions.Count);
		Debug.Log (""+randomIndex+", "+questionList.questions);
		return questionList.questions [randomIndex];
	}
}
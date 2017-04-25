using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;


[System.Serializable]
class Question {
	public string correct;
	public string incorrect;
	public string imageID;
}
[System.Serializable]
class QuestionList {
	public List<Question> questions;
}


public class content : MonoBehaviour {

	private QuestionList questionList;
	void Start () {
		TextAsset asset = Resources.Load (Path.Combine ("Questions", "questions")) as TextAsset;
		questionList = JsonUtility.FromJson<QuestionList> (asset.text);
		Debug.Log ("apekatt");
		Debug.Log (questionList.questions [0].correct);
	}

	// Update is called once per frame
	void Update () {

	}

	public Question getQuestion() {
		int randomIndex = Random.Range (0, questionList.questions.Count);
		return questionList.questions [randomIndex];
	}
}
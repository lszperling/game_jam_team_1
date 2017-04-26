using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RulesMenuLogic : MonoBehaviour {

	public Button BackToMenuButton;

	// Use this for initialization
	void Start () {
		BackToMenuButton.onClick.AddListener(() => BackToMenu());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void BackToMenu(){

		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 

	}
}

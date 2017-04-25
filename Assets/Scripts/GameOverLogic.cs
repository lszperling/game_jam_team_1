using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverLogic : MonoBehaviour {

	public Button RestartButton;
	public Button QuitButton;

	// Use this for initialization
	void Start () {

		RestartButton.onClick.AddListener(() => StartGame());
		QuitButton.onClick.AddListener(() => QuitGame());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void StartGame(){
		SceneManager.LoadScene("Game", LoadSceneMode.Single); 
	}

	private void QuitGame(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 

	}
}

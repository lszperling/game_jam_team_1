using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverLogic : MonoBehaviour {

	public Button RestartButton;
	public Button ShareButton;
	public Button QuitButton;

	// Use this for initialization
	void Start () {
		RestartButton.GetComponent<Animator> ().SetTrigger ("breathe");
		RestartButton.onClick.AddListener(() => StartGame());
		ShareButton.onClick.AddListener(() => Share());
		QuitButton.onClick.AddListener(() => QuitGame());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Share() {
		
	}

	private void StartGame(){
		SceneManager.LoadScene("Game", LoadSceneMode.Single); 
	}

	private void QuitGame(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 

	}
}
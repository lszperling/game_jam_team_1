using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {


	public Button StartGameButton;

	// Use this for initialization
	void Start () {

		StartGameButton.onClick.AddListener(() => StartGame());
		//button2.onClick.AddListener(() => ButtonClicked(button2));
	}
	
	// Update is called once per frame
	void Update () {



	}

	private void StartGame(){
	
		SceneManager.LoadScene("Game", LoadSceneMode.Single); 

	}



}

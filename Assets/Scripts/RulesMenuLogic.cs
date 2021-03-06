﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RulesMenuLogic : MonoBehaviour {

	public Button BackToMenuButton;
	public GameObject TransImage;

	private bool StartGameClicked = false;
	private bool BackToMenuClicked =  false;
	private int TransWidth;
	private int TransHeight;

	// Use this for initialization
	void Start () {
		BackToMenuButton.onClick.AddListener(() => BackToMenu());

		TransWidth = 0;
		TransHeight = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (StartGameClicked) {

			RectTransform TransRectTrans = TransImage.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {	
				SceneManager.LoadScene ("Game", LoadSceneMode.Single); 			
			} else {			
				TransHeight += 200;
				TransWidth += 200;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);			
			}		
		} 
		else if (BackToMenuClicked) {

			RectTransform TransRectTrans = TransImage.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {
				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); 
			} else {
				TransHeight += 200;
				TransWidth += 200;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);
			}		
		}
	}

	private void BackToMenu(){
		if (!StartGameClicked) {
			BackToMenuClicked = true;
		}
	}
}

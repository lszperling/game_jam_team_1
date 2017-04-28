using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsMenuLogic: MonoBehaviour {

	public Button BackButton;
	public GameObject TransImage;

	private bool BackButtonClicked = false;
	private int TransWidth;
	private int TransHeight;

	// Use this for initialization
	void Start () {
		BackButton.onClick.AddListener(() => GoBack());

		TransWidth = 0;
		TransHeight = 0;
	}

	// Update is called once per frame
	void Update () {

		if (BackButtonClicked) {

			RectTransform TransRectTrans = TransImage.GetComponent<RectTransform> ();

			if (TransRectTrans.sizeDelta.x >= 3400 && TransRectTrans.sizeDelta.x >= 3400) {
				SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single); 
			} else {
				TransHeight += 200;
				TransWidth += 200;
				TransRectTrans.sizeDelta = new Vector2 (TransWidth, TransHeight);
			}		
		}

	}
	private void GoBack(){
		BackButtonClicked = true;
	}
}

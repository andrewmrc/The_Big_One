using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MainMenu : MonoBehaviour {

	Fader refFader;
	public Button newGameButton;
	public GameObject mainMenuCanvas;

	public void Start () {
		refFader = FindObjectOfType<Fader>();
		newGameButton.Select ();
	}

	public void NewGameBtn(string newGameLevel)
    {
		refFader.GetComponent<Canvas> ().sortingOrder = 10;
		refFader.StartCoroutine(refFader.FadeIn());
		StartCoroutine (LoadScene (newGameLevel));
    }


	public void LoadGameBtn()
	{
		refFader.GetComponent<Canvas> ().sortingOrder = 10;
		mainMenuCanvas.transform.GetComponent<Settings> ().loadText.SetActive (true);
		refFader.StartCoroutine(refFader.FadeIn());
		StartCoroutine(EndLoad());
	}


	IEnumerator EndLoad () {
		yield return new WaitForSeconds (3f);
		mainMenuCanvas.transform.GetComponent<Settings> ().LoadFunction (0);
	}


    public void ExitGameBtn()
    {
        Application.Quit();
    }


	IEnumerator LoadScene (string levelName) {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (levelName);
	}
}

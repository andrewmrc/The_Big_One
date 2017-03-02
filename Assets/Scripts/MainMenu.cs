using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	Fader refFader;
	public Button newGameButton;

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

    public void ExitGameBtn()
    {
        Application.Quit();
    }


	IEnumerator LoadScene (string levelName) {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (levelName);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	Fader refFader;

	public void Start () {
		refFader = FindObjectOfType<Fader>();
	}

	public void NewGameBtn(string newGameLevel)
    {
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

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour {

	Fader refFader;

	public void Start () {
		refFader = FindObjectOfType<Fader>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene ("Level_Office"));
		} else if(Input.GetKeyDown(KeyCode.L)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene ("Level_Hospital"));
		} else if(Input.GetKeyDown(KeyCode.I)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene ("Main_Menu"));
		} else if(Input.GetKeyDown(KeyCode.O)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (SceneManager.GetActiveScene().name));
		}
	}


	IEnumerator LoadScene (string levelName) {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (levelName);
	}
}

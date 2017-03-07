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
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (0));
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (2));
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (5));
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (7));
		} else if(Input.GetKeyDown(KeyCode.Alpha5)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (8));
		} else if(Input.GetKeyDown(KeyCode.Alpha6)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (9));
		} else if(Input.GetKeyDown(KeyCode.Alpha7)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (11));
		} else if(Input.GetKeyDown(KeyCode.Alpha8)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (14));
		} else if(Input.GetKeyDown(KeyCode.Alpha9)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (15));
		} else if(Input.GetKeyDown(KeyCode.Alpha0)) {
			refFader.StartCoroutine(refFader.FadeIn());
			StartCoroutine (LoadScene (SceneManager.GetActiveScene().buildIndex));
		}
	}


	IEnumerator LoadScene (int sceneIndex) {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (sceneIndex);
	}
}

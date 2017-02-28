using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    public GameObject fade;

	//Serve a fare un FadeOut all'inizio delle scene
	void OnEnable() {
		fade.GetComponent<CanvasGroup> ().alpha = 1;
		StartCoroutine(FadeOut());
	}

	public void StartFadeIn () {
		fade.GetComponent<CanvasGroup> ().alpha = 0;
		StartCoroutine(FadeIn());
	}

	public void StartFadeOut () {
		fade.GetComponent<CanvasGroup> ().alpha = 1;
		StartCoroutine(FadeOut());
	}

    public IEnumerator FadeIn()
    {
        while (fade.GetComponent<CanvasGroup>().alpha < 1)
        {
            fade.GetComponent<CanvasGroup>().alpha += 1f * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (fade.GetComponent<CanvasGroup>().alpha > 0)
        {
            fade.GetComponent<CanvasGroup>().alpha -= 1f * Time.deltaTime;
            yield return null;
        }
    }
}

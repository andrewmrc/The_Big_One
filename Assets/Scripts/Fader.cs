using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    public GameObject fade;

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

using UnityEngine;
using System.Collections;

public class TextPrinter : MonoBehaviour {

    float time = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator TextPrint(string text)
    {
        time += Time.deltaTime;

        while (time < 2f)
        {

        }
        time = 0f;
        yield return null;
    }
}

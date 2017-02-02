using UnityEngine;
using System.Collections;
using System;

public class Examinable : ExamineAbstract {

    Coroutine ClickCO;
    

    public override void ClickMe()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(gameObject.name);
        }
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator DoStuff()
    {
        while (true)
        {
            Debug.Log(gameObject.name);
            yield return null;
        }
        
    }
    public override void StopClickMe()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}

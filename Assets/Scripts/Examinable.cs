using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;

public class Examinable : ExamineAbstract {

    Coroutine ClickCO;
	UI refUI;
	bool isLooking = false;
	bool isClicked = false;
	public Sprite memorySprite;
	public string descriptionText;
	public UnityEvent returnEvent;

    // Use this for initialization
    void Start () {
		refUI = FindObjectOfType<UI>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override void ClickMe()
	{
		if (!isClicked) {
			this.transform.GetChild(0).gameObject.SetActive(true);
		}

		if (!isClicked && Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine"))
		{
			Debug.Log ("Premo Esamina!");
			isClicked = true;
			isLooking = false;
			refUI.ExamineMemory(memorySprite, true);
			//refUI.ExaminableText(isLooking);
			this.transform.GetChild(0).gameObject.SetActive(false);

			refUI.TextToShow(descriptionText, true);
			Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
			GameManager.Self.blockMovement = true;

			GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;

			GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = false;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Turn", 0);

			returnEvent.Invoke ();

			StartCoroutine(StopExamination());
		} else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine") && isClicked)
		{
			isClicked = false;
			//isLooking = true;
			StopAllCoroutines();
			refUI.ExamineMemory(memorySprite, false);
			refUI.TextToShow(descriptionText, false);
			Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
			GameManager.Self.blockMovement = false;
			GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
		}


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


	IEnumerator StopExamination () {
		yield return new WaitForSeconds (3f);
		isClicked = false;


		refUI.ExamineMemory(memorySprite, false);
		refUI.TextToShow(descriptionText, false);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
		GameManager.Self.blockMovement = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
	}

}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;

public class ObjectExaminator : MonoBehaviour {

	public Sprite memorySprite;

	bool isLooking = false;
	bool isClicked = false;

	UI refUI;

	RaycastHit hit;
	public bool isIn = false;
	Shader outline;
	Shader nullMaterial;

	public string text;

	public UnityEvent returnEvent;

	//public ReorderableEventList Events;

	void Start ()
	{
        text = text.Replace("__", "\n");
		refUI = FindObjectOfType<UI>();
		outline = Resources.Load("Outline") as Shader;
		nullMaterial = Resources.Load("Null") as Shader;
	}



	void OnTriggerEnter(Collider player)
	{
		if (player.tag == "Player")
		{
			isIn = true;
			//this.transform.GetChild(0).gameObject.SetActive(true);
			StartCoroutine(ClickMe(0));
			ChangeMaterial(isIn);

		}       

	}

	void OnTriggerExit(Collider player)
	{

		if (player.tag == "Player")
		{
			//refUI.ExaminableText(false);
			this.transform.GetChild(0).gameObject.SetActive(false);

			isIn = false;
			refUI.ExamineMemory(null, false);
			ChangeMaterial(isIn);


		}


	}


	IEnumerator LookMe(float delay)
	{
		while (isIn)
		{
			yield return new WaitForSeconds(delay);

			if (!isClicked)
			{
				refUI.ExamineMemory(null, false);

				if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine"))
				{
					isClicked = true;
					this.transform.GetChild(0).gameObject.SetActive(false);

					refUI.ExamineMemory(memorySprite, true);
					refUI.TextToShow(text, true);
					Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
					GameManager.Self.blockMovement = true;

					GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;

					GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = false;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);
					returnEvent.Invoke ();

				}


			}
			else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine") && isClicked)
			{
				isClicked = false;
				this.transform.GetChild(0).gameObject.SetActive(isLooking);

				refUI.ExamineMemory(memorySprite, false);
				refUI.TextToShow(text, false);
				Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
				GameManager.Self.blockMovement = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
			}

		}
		this.transform.GetChild(0).gameObject.SetActive(false);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
	}


	IEnumerator ClickMe(float delay)
	{
		while (isIn)
		{
			isLooking = false;
			yield return new WaitForSeconds(delay);
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
			if (Physics.Raycast(ray, out hit, 5f))
			{

				Debug.DrawLine(ray.origin, hit.point, Color.magenta);
				if (hit.collider.GetComponent<ObjectExaminator>() && !isClicked)
				{
					refUI.ExamineMemory(null, false);
					isLooking = true;

					if (!isClicked && Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine"))
					{
						Debug.Log ("Premo Esamina!");
						isClicked = true;
						isLooking = false;
						refUI.ExamineMemory(memorySprite, true);
						//refUI.ExaminableText(isLooking);
						this.transform.GetChild(0).gameObject.SetActive(false);

						refUI.TextToShow(text, true);
						Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
						GameManager.Self.blockMovement = true;

						GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;

						GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = false;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);
						//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Turn", 0);

						returnEvent.Invoke ();

						StartCoroutine(StopExamination());
						/*
						foreach (var action in Events.List) {
							action.Execute ();
						}*/
	
					}


				}
				else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine") && isClicked)
				{
					isClicked = false;
					//isLooking = true;
					this.transform.GetChild(0).gameObject.SetActive(true);
					refUI.ExamineMemory(memorySprite, false);
					refUI.TextToShow(text, false);
					Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
					GameManager.Self.blockMovement = false;
					GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
				}
				//refUI.ExaminableText(isLooking);

			}

			this.transform.GetChild(0).gameObject.SetActive(isLooking);

		}
		//refUI.ExaminableText(false);
		this.transform.GetChild(0).gameObject.SetActive(false);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
	}


	void ChangeMaterial(bool _isIn)
	{
		if (_isIn)
		{

			GetComponent<MeshRenderer>().materials[1].shader = outline;
		}
		else
		{
			GetComponent<MeshRenderer>().materials[1].shader = nullMaterial;

		}
	}


	IEnumerator StopExamination () {
		yield return new WaitForSeconds (3f);
		isClicked = false;
		//isLooking = true;
		this.transform.GetChild(0).gameObject.SetActive(true);
		refUI.ExamineMemory(memorySprite, false);
		refUI.TextToShow(text, false);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
		GameManager.Self.blockMovement = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
	}
}
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class Examinable : ExamineAbstract
{
    private Coroutine ClickCO;
    private UI refUI;
    private bool isLooking = false;
    private bool isClicked = false;
    public Sprite imageToShow;
    public string descriptionText;
    public UnityEvent returnEvent;
	public bool executed;
    public bool autoClose;
	public List<MultiUseItem> personalNPC;
	private bool findSpecial = false;
	private int indexSpecial;

    // Use this for initialization
    private void Start()
    {
		descriptionText = descriptionText.Replace("__", "\n");
        refUI = FindObjectOfType<UI>();
    }


    public override void ClickMe()
    {
        if (!isClicked)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            if (this.transform.childCount == 3)
            {
                this.transform.GetChild(2).gameObject.SetActive(true);
            }
        }


        if (!isClicked)
        {
			if (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) {
				//Debug.Log ("Premo Esamina!");
				isClicked = true;
				//isLooking = false;
				GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (true);
				this.transform.GetChild (0).gameObject.SetActive (false);
				if (this.transform.childCount == 3) {
					this.transform.GetChild (2).gameObject.SetActive (false);
				}
				//se l'oggetto da esaminare ha un immagine da vedere allora stoppiamo il tempo e blurriamo la camera attivando il pannello UI dedicato e passandogli l'immagine
				if (imageToShow != null) {
					Time.timeScale = 0f;
					Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration> ().blur = 1f;
					refUI.ExamineObject (imageToShow, true);
					GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
				}

				Camera.main.GetComponentInParent<FreeLookCam> ().enabled = false;
				GameManager.Self.blockMovement = true;

				GameObject.FindGameObjectWithTag ("Player").GetComponent<CharController> ().enabled = false;

				GameObject.FindGameObjectWithTag ("Player").GetComponent<FSMLogic> ().enabled = false;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetFloat ("Forward", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Turn", 0);

				if (personalNPC.Count != 0) {
					for (int i = 0; i < personalNPC.Count; i++) {
						if (GameObject.FindGameObjectWithTag ("Player") == personalNPC [i].npcObject) {
							//refUI.TextToShow (personalNPC [i].returnMessage[0], true);
							GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = personalNPC [i].returnMessage[0];
							indexSpecial = i;
							//personalNPC [i].eventToActivate.Invoke ();
							StartCoroutine (StopExaminationSpecial ());
							findSpecial = true;
						}
					}
				} 

				//Se non è stato trovato un npc nella lista che ha un comportamento particolare stampiamo il messaggio di default
				if (!findSpecial) {
					//refUI.TextToShow (descriptionText, true);
					GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = descriptionText;
					/*if (!executed) {
						executed = true;
						returnEvent.Invoke ();
					}*/
				}



				//se l'oggetto non ha un immagine da vedere ma solo una descrizione possiamo farla sparire dopo pochi secondi
				if (imageToShow == null) {
					StartCoroutine (StopExamination ());
				}
			}
        }
		else if (isClicked)
        {
			if (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) {
				//isLooking = true;
				StopAllCoroutines ();
				refUI.ExamineObject (imageToShow, false);
				//refUI.TextToShow (descriptionText, false);
				GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = "";
				GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
				Camera.main.GetComponentInParent<FreeLookCam> ().enabled = true;
				Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration> ().blur = 0.3f;
				GameManager.Self.blockMovement = false;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<FSMLogic> ().enabled = true;
				Time.timeScale = 1f;
				isClicked = false;
				findSpecial = false;
				if (!executed) {
					executed = true;
					returnEvent.Invoke ();
					if (findSpecial) {
						personalNPC [indexSpecial].eventToActivate.Invoke ();
					}
				}
				//Debug.Log ("Fine Esamina!");
			}
        }
    }

    private IEnumerator DoStuff()
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
        if (this.transform.childCount == 3)
        {
            this.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private IEnumerator StopExamination()
    {
        yield return new WaitForSeconds(3f);
		if (!executed) {
			executed = true;
			returnEvent.Invoke ();
		}
        isClicked = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
        refUI.ExamineMemory(imageToShow, false);
        //refUI.TextToShow(descriptionText, false);
		GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = "";
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
        Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>().blur = 0.3f;
        GameManager.Self.blockMovement = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
    }


	private IEnumerator StopExaminationSpecial()
	{
		yield return new WaitForSeconds(3f);
		if (!executed) {
			executed = true;
			personalNPC [indexSpecial].eventToActivate.Invoke ();
		}
		isClicked = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
		refUI.ExamineMemory(imageToShow, false);
		//refUI.TextToShow(descriptionText, false);
		GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = "";
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
		Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>().blur = 0.3f;
		GameManager.Self.blockMovement = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
	}


	public void ResetExecuted () {
		executed = false;
	}

}
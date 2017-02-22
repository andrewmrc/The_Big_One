using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.ImageEffects;

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

        if (!isClicked && Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Examine"))
        {
            Debug.Log("Premo Esamina!");
            isClicked = true;
            isLooking = false;

            this.transform.GetChild(0).gameObject.SetActive(false);
            if (this.transform.childCount == 3)
            {
                this.transform.GetChild(2).gameObject.SetActive(false);
            }
            //se l'oggetto da esaminare ha un immagine da vedere allora stoppiamo il tempo e blurriamo la camera attivando il pannello UI dedicato e passandogli l'immagine
            if (imageToShow != null)
            {
                Time.timeScale = 0f;
                Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>().blur = 1f;
                refUI.ExamineObject(imageToShow, true);
            }

            Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
            GameManager.Self.blockMovement = true;

            GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;

            GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Turn", 0);

            refUI.TextToShow(descriptionText, true);

			if (!executed) {
				executed = true;
				returnEvent.Invoke ();
			}

            //se l'oggetto non ha un immagine da vedere ma solo una descrizione possiamo farla sparire dopo pochi secondi
            if (imageToShow == null)
            {
                StartCoroutine(StopExamination());
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Examine") && isClicked)
        {
            isClicked = false;
            //isLooking = true;
            StopAllCoroutines();
            refUI.ExamineObject(imageToShow, false);
            refUI.TextToShow(descriptionText, false);
            Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
            Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>().blur = 0.3f;
            GameManager.Self.blockMovement = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
            Time.timeScale = 1f;
			Debug.Log("Fine Esamina!");
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
        isClicked = false;

        refUI.ExamineMemory(imageToShow, false);
        refUI.TextToShow(descriptionText, false);
        Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
        Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>().blur = 0.3f;
        GameManager.Self.blockMovement = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FSMLogic>().enabled = true;
    }
}
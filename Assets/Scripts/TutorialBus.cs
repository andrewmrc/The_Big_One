using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class TutorialBus : MonoBehaviour {

	private Camera cameraMain;
	private GameObject pivot;
	private GameObject rig;

	private bool farTutorial;
	private bool secondBody;
	private bool nearTutorial;
	private bool firstBody;
	private bool canFinish;

	public GameObject ui_Concentrati;
	public GameObject finalEvent;

	// Use this for initialization
	void Start () {
		cameraMain = Camera.main;
		pivot = cameraMain.transform.parent.gameObject;
		rig = pivot.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("NPC_Passenger_Far").tag == "Player" /*|| GameObject.Find ("NPC_Passenger_Mid").tag == "Player"*/) {
			if (GameManager.Self.outOfYourBody && !secondBody) {
				StartCoroutine (SecondBodyControl ());
			}
			pivot.transform.localPosition = new Vector3 (0, 1.8f, 0);
		} else {
			pivot.transform.localPosition = new Vector3 (0, 1.6f, 0);
		}

		if (Input.GetMouseButton (1) || (Input.GetAxis ("LeftTriggerJoystick") >= 0.001)) {
			RaycastHandler ();
			ui_Concentrati.SetActive (false);
		} else {
			//ui_Concentrati.SetActive (true);
		}

		if (GameManager.Self.outOfYourBody && !firstBody) {
			StartCoroutine (FirstBodyControl ());
		}


		if (GameManager.Self.playerBody == GameObject.FindGameObjectWithTag ("Player") && canFinish) {
			finalEvent.GetComponent<GameEvents> ().ExecuteNTimes (1);
		}

	}


	public void RaycastHandler()
	{
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.DrawLine(ray.origin, hit.point, Color.black);
			if (hit.collider.name == "NPC_Passenger_Far" && GameObject.FindGameObjectWithTag("Player") == GameManager.Self.playerBody && !farTutorial) {
				StartCoroutine (FarTutorial ());
				/*} else if (hit.collider.name == "NPC_Passenger_Mid" && !secondBody) {
				StartCoroutine (MidTutorial ());*/

			} else if (hit.collider.name == "NPC_Passenger_Near" && !nearTutorial) {
				StartCoroutine (NearTutorial ());
			}
		}
	}


	public IEnumerator FarTutorial(){
		farTutorial = true;
		GameManager.Self.SetPlayerState (1);
		//rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Non riesco a percepire fino a quella distanza. \n Devo provare con qualcuno più vicino.";
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		//rig.GetComponent<FreeLookCam> ().enabled = true;
		GameManager.Self.SetPlayerState (0);
	}


	public IEnumerator NearTutorial(){
		nearTutorial = true;
		GameManager.Self.SetPlayerState (1);
		//rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Percepisco una connessione con queste persone. \n Devo focalizzare i miei pensieri.";
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		//rig.GetComponent<FreeLookCam> ().enabled = true;
		GameManager.Self.SetPlayerState (0);
	}


	public IEnumerator FirstBodyControl(){
		firstBody = true;
		//yield return new WaitForSeconds (1f);
		GameManager.Self.SetPlayerState (1);
		//rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Possession.SetActive(false);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Incredibile, sono entrata nel corpo di un'altra persona! \n Ci sono davvero riuscita!";
		yield return new WaitForSeconds (4f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		//rig.GetComponent<FreeLookCam> ().enabled = true;
		GameManager.Self.SetPlayerState (0);
	}


	public IEnumerator SecondBodyControl(){
		secondBody = true;
		canFinish = true;
		//yield return new WaitForSeconds (1f);
		GameManager.Self.SetPlayerState (1);
		//rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Possession.SetActive(false);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Anche se riesco a controllare un altro corpo, sono ancora connessa con il mio. \n Questo potere è così strano...";
		yield return new WaitForSeconds (4f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		//rig.GetComponent<FreeLookCam> ().enabled = true;
		GameManager.Self.SetPlayerState (0);
	}

}

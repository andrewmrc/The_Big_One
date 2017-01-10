using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class TutorialBus : MonoBehaviour {

	private Camera cameraMain;
	private GameObject pivot;
	private GameObject rig;

	private bool farTutorial;
	private bool midTutorial;
	private bool nearTutorial;
	private bool firstBody;

	public GameObject ui_Concentrati;

	// Use this for initialization
	void Start () {
		cameraMain = Camera.main;
		pivot = cameraMain.transform.parent.gameObject;
		rig = pivot.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("NPC_Passenger_Far").tag == "Player" || GameObject.Find ("NPC_Passenger_Mid").tag == "Player") {
			pivot.transform.localPosition = new Vector3 (0, 1.8f, 0);
		} else {
			pivot.transform.localPosition = new Vector3 (0, 1.6f, 0);
		}

		if (Input.GetMouseButton (1) || (Input.GetAxis ("LeftTriggerJoystick") >= 0.001)) {
			RaycastHandler ();
			ui_Concentrati.SetActive (false);
		} else {
			ui_Concentrati.SetActive (true);
		}

		if (GameManager.Self.outOfYourBody && !firstBody) {
			StartCoroutine (FirstBodyControl ());
		}
	}


	public void RaycastHandler()
	{
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.DrawLine(ray.origin, hit.point, Color.black);
			if (hit.collider.name == "NPC_Passenger_Far" && !farTutorial) {
				StartCoroutine (FarTutorial ());
			} else if (hit.collider.name == "NPC_Passenger_Mid" && !midTutorial) {
				StartCoroutine (MidTutorial ());

			} else if (hit.collider.name == "NPC_Passenger_Near" && !nearTutorial) {
				StartCoroutine (NearTutorial ());
			}
		}
	}


	public IEnumerator FarTutorial(){
		farTutorial = true;
		rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Non riesco a percepire fino a quella distanza. \n Devo provare con qualcuno più vicino.";
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		rig.GetComponent<FreeLookCam> ().enabled = true;
	}

	public IEnumerator MidTutorial(){
		midTutorial = true;
		rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Se le persone vicine sono rivolte verso di me non riesco a concentrarmi. \n Questo potere è così strano...";
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		rig.GetComponent<FreeLookCam> ().enabled = true;
	}

	public IEnumerator NearTutorial(){
		nearTutorial = true;
		rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Quando sono di spalle percepisco una connessione con queste persone. \n Devo focalizzare i miei pensieri.";
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		rig.GetComponent<FreeLookCam> ().enabled = true;
	}

	public IEnumerator FirstBodyControl(){
		firstBody = true;
		yield return new WaitForSeconds (1f);
		rig.GetComponent<FreeLookCam> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Incredibile, sono entrata nel corpo di un'altra persona! \n Ci sono davvero riuscita!";
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		rig.GetComponent<FreeLookCam> ().enabled = true;
	}
}

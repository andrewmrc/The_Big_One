using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class TutorialIdea : MonoBehaviour {

	private Camera cameraMain;
	private GameObject pivot;
	private GameObject rig;

	private bool farTutorial;
	private bool secondBody;
	private bool nearTutorial;
	private bool firstBody;
	private bool canFinish;

	public GameObject ui_Concentrati;
	public GameObject startEvent;

	// Use this for initialization
	void Start () {
		cameraMain = Camera.main;
		pivot = cameraMain.transform.parent.gameObject;
		rig = pivot.transform.parent.gameObject;
		//startEvent.GetComponent<GameEvents> ().ExecuteNTimes (1);
		ui_Concentrati.SetActive (false);
		StartCoroutine (IdeaTutorial ());
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetMouseButton (1) || (Input.GetAxis ("LeftTriggerJoystick") >= 0.001)) {
			//RaycastHandler ();
			ui_Concentrati.SetActive (false);
		} else {
			//ui_Concentrati.SetActive (true);
		}


	}


	public IEnumerator IdeaTutorial(){
		//farTutorial = true;
		GameManager.Self.SetPlayerState (1);
		//rig.GetComponent<FreeLookCam> ().enabled = false;
		//GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "Non riesco a percepire fino a quella distanza. \n Devo provare con qualcuno più vicino.";
		yield return new WaitForSeconds (12f);
		//GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = "";
		//rig.GetComponent<FreeLookCam> ().enabled = true;
		GameManager.Self.SetPlayerState (2);
		ui_Concentrati.SetActive (true);
	}

}

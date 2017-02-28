using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueHandler : MonoBehaviour {

	public bool cantTalk;
	//public List<string> dialogues;
	public float distanceToTalk = 1f;
	//public float smoothSpeed = 1f;
	//public string mainPhrase;
	public float dialogueSpeed = 2f;
	GameObject currentPlayer;
	public List<DialogueItem> conversations;
	public bool executed;
	public UnityEvent returnEvent;
	GameObject canvasFader;
	bool notInitialRotation;
	int playerPowerState;

	// Use this for initialization
	void Start () {
		canvasFader = GameObject.FindGameObjectWithTag ("Fader");
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log("This gameobject: " + this.gameObject.name + " is distant " + Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) + " from the player");
		if (/*this.GetComponent<FieldOfView> ().visibleTargets.Contains (GameObject.FindGameObjectWithTag ("Player").transform) && */!cantTalk) {
			//this.transform.GetChild (0).gameObject.SetActive (true);
			//this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer>().enabled = true;
			currentPlayer = GameObject.FindGameObjectWithTag ("Player");
			float distanceSqr = (this.transform.position - currentPlayer.transform.position).sqrMagnitude;
			if ((distanceSqr < distanceToTalk) && !currentPlayer.gameObject.GetComponent<FSMLogic>().isAiming && currentPlayer != this.gameObject) { //Within range
				//Debug.Log("PUOI PARLARE: " + (gameObject.name) + distanceSqr);
				this.transform.GetChild (0).gameObject.SetActive (true);
				StopAllCoroutines ();
				//this.transform.GetChild (0).gameObject.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) {
					cantTalk = true;
					GameManager.Self.blockMovement = true;
					//canvasFader.GetComponent<Fader> ().StartFadeOut ();
					if (!notInitialRotation) {
						StartCoroutine (RotateCharAnim ());
						//StartCoroutine (RotatePlayer ());
					}
					notInitialRotation = true;
					playerPowerState = (int)GameManager.Self.playerState;
					GameManager.Self.SetPlayerState (1);
					GameManager.Self.isShowMemory = true;
					//Debug.Log ("PRESS E TO TALK");
					Vector3 targetPlayer = new Vector3 (currentPlayer.transform.position.x, this.gameObject.transform.position.y, currentPlayer.transform.position.z);
					this.gameObject.transform.LookAt (targetPlayer);
					Vector3 targetNPC = new Vector3 (this.gameObject.transform.position.x, currentPlayer.transform.position.y, this.gameObject.transform.position.z);
					currentPlayer.transform.LookAt (targetNPC);
					currentPlayer.GetComponent<CharController> ().enabled = false;
					currentPlayer.GetComponent<Animator> ().SetFloat ("Forward", 0);
					StartCoroutine (DPrinter3 ());
				}
			} else {
				//Debug.Log("NON PUOI PARLARE: " + (gameObject.name) + distanceSqr);
				this.transform.GetChild (0).gameObject.SetActive (false);
				if (notInitialRotation) {
					this.GetComponent<FSM_ReturnInPosition>().enabled = true;
					this.GetComponent<FSM_ReturnInPosition> ().ResetRotation ();
					notInitialRotation = false;
				}
			}
		} else {
			//this.transform.GetChild (0).gameObject.SetActive (false);
			//this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}

	}
		

	IEnumerator RotatePlayer () {
		//Debug.Log ("ROTATE PLAYER");
		//if(currentPlayer.GetComponent<Animator>().GetFloat("Forward") <= 0.1f){
			Debug.Log ("ROTATE PLAYER");
			currentPlayer.GetComponent<Animator>().SetFloat("Forward", 1);
			yield return new WaitForSeconds(0.1f);
			currentPlayer.GetComponent<Animator>().SetFloat("Forward", 0);
		//}
	}


	IEnumerator RotateCharAnim () {
		//Debug.Log ("ROTATE NPC");
		if(this.GetComponent<Animator>().GetFloat("Forward") < 1){
			this.GetComponent<Animator>().SetFloat("Forward", 1);
			yield return new WaitForSeconds(0.1f);
			this.GetComponent<Animator>().SetFloat("Forward", 0);
		}
	}


	/*
	void FixedUpdate () {
		float distanceSqr = (this.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude;
		if(distanceSqr < distanceToTalk) { //Within range
			if (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) {
				//Debug.Log ("PRESS E TO TALK");
				Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position - this.transform.position;
				Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
				transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, smoothSpeed * Time.time);
			}
		}

	}*/

	/*
	IEnumerator DPrinter () {
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = mainPhrase;
		yield return new WaitForSeconds (5f);
		cantTalk = false;
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = "";
		GameManager.Self.blockMovement = false;

	}


	IEnumerator DPrinter2 () {
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = false;

		for(int i = 0; i < dialogues.Count; i++) {
			GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = dialogues[i];
			yield return new WaitForSeconds (2f);

		}

		cantTalk = false;
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = "";
		GameManager.Self.blockMovement = false;
		returnEvent.Invoke ();
	}*/


	IEnumerator DPrinter3 () {
		this.transform.GetChild (0).gameObject.SetActive (false); //.GetComponent<MeshRenderer> ().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (true);
		for(int i = 0; i < conversations.Count; i++) {
			if (conversations [i].npcSpeaker != null) {
				if (conversations [i].npcSpeaker.name == currentPlayer.name) {
					for (int j = 0; j < conversations [i].dialogues.Count; j++) {
						Debug.Log ("i: " + i + ", j: " + j);

						GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = conversations [i].dialogues [j];

						float seconds = dialogueSpeed;
						bool isFirstClick = false;
						if (conversations [i].audioSpeech.Count != 0 && conversations [i].audioSpeech [j] != null) {
							AudioSource audioSource = this.gameObject.AddComponent<AudioSource> ();
							audioSource.clip = conversations [i].audioSpeech [j];
							audioSource.Play ();
							float audioDuration = audioSource.clip.length;

							seconds = audioDuration;
						}  


						//Play speech e quando finisce passa al prossimo quindi waitforsecond inserendo la durata dell'audio
						while (seconds > 0) {
							seconds -= Time.deltaTime;
							if((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine")) && isFirstClick){
								seconds = 0;
							}
							isFirstClick = true;
							yield return null;
						}

					}
					if (!conversations[i].executed) {
						Debug.Log ("eseguito");
						conversations[i].executed = true;
						conversations[i].eventToActivate.Invoke ();
					}
					break;
				} else {
					Debug.Log ("NESSUN DIALOGO ASSEGNATO TROVATO");
				}
			} else {
				for (int j = 0; j < conversations [i].dialogues.Count; j++) {
					//Debug.Log ("i: " + i + ", j: " + j);

					GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = conversations [i].dialogues [j];
					float seconds = dialogueSpeed;
					bool isFirstClick = false;
					if (conversations [i].audioSpeech.Count != 0 && conversations [i].audioSpeech [j] != null) {
						AudioSource audioSource = this.gameObject.AddComponent<AudioSource> ();
						audioSource.clip = conversations [i].audioSpeech [j];
						audioSource.Play ();
						float audioDuration = audioSource.clip.length;

						seconds = audioDuration;
					}  


					//Play speech e quando finisce passa al prossimo quindi waitforsecond inserendo la durata dell'audio
					while (seconds > 0) {
						seconds -= Time.deltaTime;
						if((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine")) && isFirstClick){
							seconds = 0;
						}
						isFirstClick = true;
						yield return null;
					}

				}
				break;
			}

		}
		GameManager.Self.SetPlayerState (playerPowerState);
		GameManager.Self.isShowMemory = false;
		cantTalk = false;
		this.transform.GetChild (0).gameObject.SetActive (true); //.GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = "";
		GameManager.Self.blockMovement = false;
		if (!executed) {
			executed = true;
			returnEvent.Invoke ();
		}
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
	}
}

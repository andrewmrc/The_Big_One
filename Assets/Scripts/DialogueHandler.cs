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

	public List<DialogueItem> conversations;

	public UnityEvent returnEvent;

	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log("This gameobject: " + this.gameObject.name + " is distant " + Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) + " from the player");
		if (this.GetComponent<FieldOfView> ().visibleTargets.Contains (GameObject.FindGameObjectWithTag ("Player").transform) && !cantTalk) {
			//this.transform.GetChild (0).gameObject.SetActive (true);
			//this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer>().enabled = true;
			float distanceSqr = (this.transform.position - GameObject.FindGameObjectWithTag ("Player").transform.position).sqrMagnitude;
			if (distanceSqr < distanceToTalk) { //Within range
				//this.transform.GetChild (0).gameObject.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) {
					cantTalk = true;
					//Debug.Log ("PRESS E TO TALK");
					//Vector3 targetPlayer = new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, this.gameObject.transform.position.y, GameObject.FindGameObjectWithTag ("Player").transform.position.z);
					//this.gameObject.transform.LookAt (targetPlayer);
					Vector3 targetNPC = new Vector3 (this.gameObject.transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y, this.gameObject.transform.position.z);
					GameObject.FindGameObjectWithTag ("Player").transform.LookAt (targetNPC);
					GameManager.Self.blockMovement = true;
					GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);
					StartCoroutine(DPrinter3());
				}
			}
		} else {
			//this.transform.GetChild (0).gameObject.SetActive (false);
			//this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer>().enabled = false;
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
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = false;

		for(int i = 0; i < conversations.Count; i++) {
			if (conversations [i].npcSpeaker != null) {
				if (conversations [i].npcSpeaker.name == GameObject.FindGameObjectWithTag ("Player").name) {
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
					break;
				} else {
					Debug.Log ("NESSUN DIALOGO ASSEGNATO TROVATO");
				}
			} else {
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
						if(Input.GetKeyDown(KeyCode.E) && isFirstClick){
							seconds = 0;
						}
						isFirstClick = true;
						yield return null;
					}

				}
				break;
			}

		}

		cantTalk = false;
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = "";
		GameManager.Self.blockMovement = false;
		returnEvent.Invoke ();

	}
}

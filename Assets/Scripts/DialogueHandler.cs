using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueHandler : MonoBehaviour {

	public bool cantTalk;
	public List<string> dialogues;
	public float distanceToTalk = 1f;
	public float smoothSpeed = 1f;
	public string mainPhrase;

	public UnityEvent returnEvent;


	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log("This gameobject: " + this.gameObject.name + " is distant " + Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) + " from the player");
		if (this.GetComponent<FieldOfView> ().visibleTargets.Contains (GameObject.FindGameObjectWithTag ("Player").transform) && !cantTalk) {
			this.transform.GetChild (0).gameObject.SetActive (true);
			float distanceSqr = (this.transform.position - GameObject.FindGameObjectWithTag ("Player").transform.position).sqrMagnitude;
			if (distanceSqr < distanceToTalk) { //Within range
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
					StartCoroutine(DPrinter2());
				}
			}
		} else {
			this.transform.GetChild (0).gameObject.SetActive (false);

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


		//GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = mainPhrase;

		for(int i = 0; i < dialogues.Count; i++) {
			GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = dialogues[i];
			yield return new WaitForSeconds (2f);

		}

		//yield return new WaitForSeconds (5f);
		cantTalk = false;
		this.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = true;
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = "";
		GameManager.Self.blockMovement = false;
		returnEvent.Invoke ();
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueHandler : MonoBehaviour {

	public bool canTalk;
	public List<string> dialogues;
	public float distanceToTalk = 1f;
	public float smoothSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	/*
	// Update is called once per frame
	void Update () {
		//Debug.Log("This gameobject: " + this.gameObject.name + " is distant " + Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) + " from the player");
		//if (this.GetComponent<FieldOfView> ().visibleTargets.Contains (GameObject.FindGameObjectWithTag ("Player").transform)) {
			float distanceSqr = (this.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).sqrMagnitude;
			if(distanceSqr < distanceToTalk) { //Within range
				if (Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) {
					//Debug.Log ("PRESS E TO TALK");
					this.gameObject.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform);
					GameObject.FindGameObjectWithTag ("Player").transform.LookAt (this.gameObject.transform);
				}
			}
		//}
	}*/


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

	}

}

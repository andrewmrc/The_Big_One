﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/*
[Serializable]
public class SpawnEvent : UnityEvent <GameObject, Transform> {}*/

public class GameEvents : MonoBehaviour {


	public enum Condition {Spawner, PlayAnimationFloat, PlayAnimationBool, LoadScene }
	public Condition whichEvent;

	public GameObject objectToUse;
	public Transform positionToSpawn;
	public string animationName;
	public float animationValueFloat;
	public bool animationValueBool;

	public int n;
	public int i = 0;
	//public SpawnEvent spawner;

	 void Spawner () {
		Instantiate (objectToUse).transform.position = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, positionToSpawn.position.z);
		//StartCoroutine (SpawnerRoutine ());
	}

	 void PlayAnimationFloat () {
		objectToUse.GetComponent<Animator> ().SetFloat (animationName, animationValueFloat);
		Debug.Log ("PLAYANIMFLOAT" + animationName + animationValueFloat);
	}

	 void PlayAnimationBool () {
		objectToUse.GetComponent<Animator> ().SetBool (animationName, animationValueBool);
		Debug.Log ("PLAYANIMBOOL");
	}


	IEnumerator SpawnerRoutine () {
		//Instantiate (objectToUse).transform.position = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, positionToSpawn.position.z);
		//objectToUse.GetComponent<CharController> ().enabled = true;
		yield return new WaitForSeconds (2f);
		//objectToUse.GetComponent<CharController> ().enabled = false;
		Debug.Log ("SPAWNA");
	}




	public void ExecuteNTimes (int n) {
		Debug.Log (i > n);
		Debug.Log (i);
		if (i<n) {			
			i++;
			Debug.Log (i);
			switch (whichEvent) {

				case Condition.PlayAnimationBool:
					PlayAnimationBool ();
					break;
				case Condition.PlayAnimationFloat:
					PlayAnimationFloat ();
					break;
				case Condition.Spawner:
					Spawner ();
					break;		
				case Condition.LoadScene:
					Spawner ();
					break;		
				default:
					break;
			}
		}
	}
}

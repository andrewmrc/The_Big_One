using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/*
[Serializable]
public class SpawnEvent : UnityEvent <GameObject, Transform> {}*/

public class GameEvents : MonoBehaviour {


	public enum Condition {Spawner, PlayAnimationFloat, PlayAnimationBool }
	public Condition whichEvent;

	public GameObject objectToUse;
	public Transform positionToSpawn;
	public string animationName;
	public float animationValueFloat;
	public bool animationValueBool;

	//public SpawnEvent spawner;

	public void Spawner () {
		Instantiate (objectToUse).transform.position = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, positionToSpawn.position.z);
		Debug.Log ("SPAWNA");
	}

	public void PlayAnimationFloat () {
		objectToUse.GetComponent<Animator> ().SetFloat (animationName, animationValueFloat);
		Debug.Log ("PLAYANIMFLOAT" + animationName + animationValueFloat);
	}

	public void PlayAnimationBool () {
		objectToUse.GetComponent<Animator> ().SetBool (animationName, animationValueBool);
		Debug.Log ("PLAYANIMBOOL");
	}




}

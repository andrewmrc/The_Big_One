using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/*
[Serializable]
public class SpawnEvent : UnityEvent <GameObject, Transform> {}*/

public class GameActions : MonoBehaviour {


	public void Spawner (GameObject item, Transform pos) {
		Instantiate (item).transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
		Debug.Log ("SPAWNA");
	}

	public void PlayAnimationFloat (GameObject item, string animationName, float animationValueFloat) {
		item.GetComponent<Animator> ().SetFloat (animationName, animationValueFloat);
		Debug.Log ("PLAYANIMFLOAT" + animationName + animationValueFloat);
	}

	public void PlayAnimationBool (GameObject item, string animationName, bool animationValueBool) {
		item.GetComponent<Animator> ().SetBool (animationName, animationValueBool);
		Debug.Log ("PLAYANIMFLOAT" + animationName + animationValueBool);
	}

    public void RandomActionSequence (string sequenceName, int arrayPosition)
    {
        FlowManager.Self.ExecuteRandomEvent(sequenceName, arrayPosition);
    }


}

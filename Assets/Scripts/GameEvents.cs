using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour {

	public void SetBool (bool asd) {

	}

	public void SpawnerProva (GameObject itemToSpawn) {

	}

	public void Spawner (GameObject itemToSpawn, Transform spawnPosition) {
		Instantiate (itemToSpawn).transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
	}

	public void PlayAnimationFloat (GameObject item, string animationName, float value) {
		item.GetComponent<Animator> ().SetFloat (animationName, value);
	}

	public void PlayAnimationBool (GameObject item, string animationName, bool state) {
		item.GetComponent<Animator> ().SetBool (animationName, state);
	}

}

using UnityEngine;
using System.Collections;

public class TestEvents : MonoBehaviour {

	public ReorderableEventList eventList;

	// Use this for initialization
	void Start () {
		eventList.List [0].Execute ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spawner (GameObject itemToSpawn, Transform spawnPosition) {
		GameObject newItem = Instantiate (itemToSpawn);
		newItem.transform.position = spawnPosition.position;

	}
}

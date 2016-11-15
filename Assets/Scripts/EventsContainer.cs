using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class MyEvent {
	public enum Condition {Spawner, PlayAnimationFloat, PlayAnimationBool }
	public Condition whichEvent;

	public GameObject objectToUse;
	public Transform positionToSpawn;
	public string animationName;
	public float animationValueFloat;
	public bool animationValueBool;


}

[Serializable]
public class EventsContainer : MonoBehaviour {
	
	public MyEvent[] events;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




}

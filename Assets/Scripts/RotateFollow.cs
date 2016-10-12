using UnityEngine;
using System.Collections;

public class RotateFollow : MonoBehaviour {

	//public int rotSpeed = 3;

	//private GameObject target;
	private Vector3 targetPoint;
	private Quaternion targetRotation;

	void  Start (){	
		//target = GameObject.FindWithTag("MainCamera");
	}
	
	void  Update (){
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}

}

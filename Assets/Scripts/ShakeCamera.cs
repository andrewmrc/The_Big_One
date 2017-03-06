using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

	public bool shakeTime;
	public float shakeAmount = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeTime == true)
		{
			Camera.main.transform.root.position = Random.onUnitSphere * shakeAmount;
		}else{
			//Camera.main.transform.localPosition = Vector3.zero;
		}
	}

	public void SetShakeTrue (bool value) {
		shakeTime = value;
	}

}

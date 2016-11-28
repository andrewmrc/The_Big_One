using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour {

	public float doorRotation;
    private Vector3 defaultRot;
    private Vector3 openRot;
    
	// Use this for initialization
	void Start () {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + doorRotation, defaultRot.z);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision coll) {
		if(coll.gameObject.tag == "Player") {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, 1);
            /*Debug.Log("APRI PORTA");

			ContactPoint contact = coll.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;

			Debug.Log (coll.contacts [0].point);
			Debug.Log ("ROT: " + rot);
			//this.gameObject.transform.rotation = Quaternion.Euler (this.gameObject.transform.rotation.x - 90f, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z);
			this.gameObject.transform.rotation = rot;*/



        }
	}

}

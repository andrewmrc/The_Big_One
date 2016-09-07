using UnityEngine;
using System;
using System.Collections;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class BodyControlPower : MonoBehaviour {

	public GameObject cameraRig;
	public GameObject mainCamera;
	private Ray m_Ray;                        // the ray used in the lateupdate for casting between the player and his target
	private RaycastHit[] m_Hits;              // the hits between the player and his target
	private RaycastHit hitInfo;
	private RayHitComparer m_RayHitComparer;  // variable to compare raycast hit distances
	private Vector3 dir;
	public bool visualiseInEditor;                  // toggle for visualising the algorithm through lines for the raycast in the editor

	// Use this for initialization
	void Start () {
		cameraRig = GameObject.FindGameObjectWithTag ("CameraRig");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		dir = transform.TransformDirection(Vector3.forward);

		// create a new RayHitComparer
		m_RayHitComparer = new RayHitComparer();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.L)) {
			RaycastHandler ();
		} else {
			cameraRig.transform.GetChild (0).GetChild (0).transform.localPosition = new Vector3 (0, 0, -1f);
			cameraRig.transform.GetComponent<ProtectCameraFromWallClip> ().enabled = true;

		}

	}


	public void RaycastHandler () {
		cameraRig.transform.GetComponent<ProtectCameraFromWallClip> ().enabled = false;
		cameraRig.transform.GetChild (0).GetChild (0).transform.localPosition = new Vector3 (0.4f, 0.1f, -0.7f);
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));;
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			Debug.DrawLine (ray.origin, hit.point);
			Debug.Log (hit.collider.name + ", " + hit.collider.tag);
			if (hit.collider.tag == "ControllableNPC") {
				if (Input.GetKeyDown (KeyCode.Space)) {
					this.gameObject.tag = "ControllableNPC";
					this.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = false;
					this.gameObject.transform.GetComponent<BodyControlPower> ().enabled = false;

					cameraRig.transform.GetComponent<AbstractTargetFollower> ().m_Target = null;
					hit.collider.gameObject.tag = "Player";
					hit.collider.transform.GetComponent<ThirdPersonUserControl> ().enabled = true;
					hit.collider.transform.GetComponent<BodyControlPower> ().enabled = true;

				}
			}
		}
	}


	// comparer for check distances in ray cast hits
	public class RayHitComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			return ((RaycastHit) x).distance.CompareTo(((RaycastHit) y).distance);
		}
	}
}

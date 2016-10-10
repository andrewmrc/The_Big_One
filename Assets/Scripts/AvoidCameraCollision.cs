using UnityEngine;
using System.Collections;

public class AvoidCameraCollision : MonoBehaviour {

	private Transform m_Pivot; 						//used as the focal rotation point, and raycast point | must be centered on the player(x and z)
	public float clipMoveTime = 0.05f;              // time taken to move when avoiding clipping (low value = fast, which it should be)
	public float returnTime = 0.5f;                 // time taken to move back towards desired position, when not clipping (typically should be a higher value than clipMoveTime)
	public float sphereCastRadius = 0.15f;           // the radius of the sphere used to test for object between camera and target
	public float closestDistance = 0.1f;            // the closest distance the camera can be from the target
	public string dontClipTag = "Player";           // don't clip against objects with this tag (useful for not clipping against the targeted object)

	private RaycastHit hit; 						//used to detect objects in front of camera
	private GameObject camFollow; 					//monitors camera's position
	private GameObject camSpot; 					//camera's destination | used for zooming camera in and out

	void  Start (){
		//set clipping planes to 0.01f
		GetComponent<Camera>().nearClipPlane = 0.01f;

		//set m_Pivot
		if(m_Pivot == null ) {
			m_Pivot = transform.parent.transform;
		}

		//create camSpot
		camSpot = new GameObject();
		camSpot.transform.name = "CameraSpot";
		camSpot.transform.parent = transform.parent;
		camSpot.transform.position = transform.position;        
        
		//create camFollow
		camFollow = new GameObject();
		camFollow.transform.name = "CameraFollow";
		camFollow.transform.parent = transform.parent;
		camFollow.transform.position = m_Pivot.position;
		//make sure the camFollow is looking at the camera
		camFollow.transform.LookAt(transform);
	}


	void  Update (){
		//distance between camFollow and camSpot
		float distFromCamSpot = Vector3.Distance(camFollow.transform.position, camSpot.transform.position);
		//distance between camFollow and camera
		float distFromCamera = Vector3.Distance(camFollow.transform.position, transform.position);
		//Debug.Log("distFromCamera: " + distFromCamera);

		//ShereCast from camFollow to camSpot
		if(Physics.SphereCast(camFollow.transform.position, sphereCastRadius, camFollow.transform.forward, out hit, distFromCamSpot)) {
			//CHECK IF THE CAMERA HIT THE PLAYER OR THE NPC
			if (hit.collider.CompareTag (dontClipTag) || hit.collider.CompareTag ("ControllableNPC")) {
				Debug.Log ("Hit: " + !hit.collider.CompareTag (dontClipTag) + " TAG: " + hit.transform.tag);
				//Provare a inserire un metodo per rendere invisibili gli NPC in caso vengano toccati dalla camera
			} else {
				//**MAKE SURE THE PLAYER IS NOT BETWEEN THE FOCUS-POINT AND CAMERA**

				//get distance between camFollow and hitPoint of raycast
				float distFromHit = Vector3.Distance (camFollow.transform.position, hit.point);
				//Debug.Log("distFromHit: " + distFromHit);

				//if camera is behind an object, immediately put it in front
				if (distFromHit < distFromCamera && !hit.collider.CompareTag (dontClipTag)) {
					//if player is very close to a wall, bring camera inward, 
					//but do not exceed the camFollow's position (dont put camera in front of player)
					if (distFromCamera > closestDistance) {
						transform.position = hit.point + 0.6f * -camFollow.transform.forward;
					} else {
						transform.position = camFollow.transform.position;
					}
				} else {
					//if player is very close to a wall, bring camera inward, 
					//but do not exceed the camFollow's position (dont put camera in front of player)
					if (distFromCamera > closestDistance) {
						//Debug.Log ("Prova");
						transform.position = Vector3.MoveTowards (transform.position, hit.point + 0.6f * -camFollow.transform.forward, clipMoveTime * Time.deltaTime);
					} else {
						transform.position = Vector3.MoveTowards (transform.position, camFollow.transform.position, clipMoveTime * Time.deltaTime);
					}
				}
			}	
		}
		else {
            //ease camera back to camSpot
			if (Input.GetMouseButton(1) || (Input.GetAxis ("LeftTriggerJoystick") >= 0.001))
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0.4f, -0.4f, -0.7f), 6 * Time.deltaTime);
                
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, camSpot.transform.position, returnTime * Time.deltaTime);

            }
        }
	}
}
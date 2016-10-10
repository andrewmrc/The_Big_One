using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PermissionCheckIN : MonoBehaviour
{

    public GameObject target;
	public string thisPasskey;
	public bool targetVersion;
	public string passDescription;

    void OnCollisionEnter(Collision collision)
    {
		GameObject colObject = collision.gameObject;

		//Using Target Version
		if (targetVersion && colObject == target)
        {
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
            target.AddComponent<Badge>();
        }

		//Using passphrase
		if (!targetVersion && colObject.GetComponent<PermissionHandler> () != null) {
			for (int i = 0; i < colObject.GetComponent<PermissionHandler> ().personalPasskeys.Count; i++) {
				if (collision.gameObject.GetComponent<PermissionHandler> ().personalPasskeys [i] == thisPasskey) {
					gameObject.GetComponent<BoxCollider> ().isTrigger = true;
					break;
				} else {
					Debug.Log ("Tu non puoi passare!!!");
					GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = passDescription;
					StartCoroutine(DeactivateDescriptionUI());
				}
			}
		}



    }

    void OnTriggerExit(Collider col)
    {
		gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }


	IEnumerator DeactivateDescriptionUI () {
		yield return new WaitForSeconds (3f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = null;
	}
}

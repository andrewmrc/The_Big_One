using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PermissionCheckIN : MonoBehaviour
{

    public GameObject target; //inserire un target e attivare la bool target version per permettere l'accesso ad un solo personaggio
	public string thisPasskey;
	public bool targetVersion; // da attivare in caso si metta un unico target
	public string passDescription; //il testo che deve apparire se un personaggio prova a entrare e non ha il permesso
	public bool useOneTime; // fa in modo che una volta passato attraverso si spenga il collider
	private bool isFound;

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
					isFound = true;
					break;
				} 
			}

			if (!isFound) {
				Debug.Log ("Tu non puoi passare!!!");
				GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = passDescription;
				StartCoroutine(DeactivateDescriptionUI());
			}
		}
    }


    void OnTriggerExit(Collider col)
    {
		gameObject.GetComponent<BoxCollider>().isTrigger = false;
		isFound = false;
		//gameObject.SetActive(false);
    }


	IEnumerator DeactivateDescriptionUI () {
		yield return new WaitForSeconds (4f);
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.text = null;
		if (useOneTime) {
			gameObject.SetActive (false);
		}
	}
}

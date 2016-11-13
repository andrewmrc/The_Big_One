using UnityEditor;
using UnityEngine;
using System.Collections;

public class Player_Configurator : EditorWindow {
	//Shortcut reminder # -> Shift, & -> alt, % -> ctrl

	[MenuItem ("Andre's Tools/Player Configurator/Full Config &4")] //Add a menu item to the toolbar
	static void PlayerConfig () {
		Debug.Log ("Object Player configuration completed!");

		Selection.activeGameObject.tag = "Player";

		if (!Selection.activeGameObject.GetComponent<CharController> ()) {
			Undo.AddComponent<Rigidbody> (Selection.activeGameObject);
			Selection.activeGameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		} else {
			Debug.Log ("Rigibody already added!");
		}

		if (!Selection.activeGameObject.GetComponent<CharController> ()) {
			Undo.AddComponent<CapsuleCollider> (Selection.activeGameObject);
			//Selection.activeGameObject.GetComponent<CapsuleCollider> ().material = PhysicMaterial "MaxFriction";
			Selection.activeGameObject.GetComponent<CapsuleCollider> ().center = new Vector3(0f, 0.88f, 0f);
			Selection.activeGameObject.GetComponent<CapsuleCollider> ().radius = 0.25f;
			Selection.activeGameObject.GetComponent<CapsuleCollider> ().height = 1.75f;

		} else {
			Debug.Log ("Capsule Collider already added!");
		}

		if (!Selection.activeGameObject.GetComponent<CharController> ()) {
			Undo.AddComponent<CharController> (Selection.activeGameObject);
		} else {
			Debug.Log ("CharController already added!");
		}

		if (!Selection.activeGameObject.GetComponent<FSMLogic> ()) {
			Undo.AddComponent<FSMLogic> (Selection.activeGameObject);
			//Selection.activeGameObject.AddComponent<FSMLogic> ().enabled = false;
		} else {
			Debug.Log ("FSMLogic already added!");
		}

		if (!Selection.activeGameObject.GetComponent<State_ControlBody> ()) {
			Undo.AddComponent<State_ControlBody> (Selection.activeGameObject);
			//Selection.activeGameObject.AddComponent<State_ControlBody> ();
		} else {
			Debug.Log ("State_ControlBody already added!");
		}

		if (!Selection.activeGameObject.GetComponent<State_PowerControl> ()) {
			Undo.AddComponent<State_PowerControl> (Selection.activeGameObject);
			//Selection.activeGameObject.AddComponent<State_PowerControl> ();
		} else {
			Debug.Log ("State_PowerControl already added!");
		}


		//Per dare accesso a determinate aree
		if (!Selection.activeGameObject.GetComponent<PermissionHandler> ()) {
			Undo.AddComponent<PermissionHandler> (Selection.activeGameObject);
		} else {
			Debug.Log ("PermissionHandler already added!");
		}


	}
		

    [MenuItem ("Andre's Tools/Player Configurator/Remove Components")]
	static void RemoveComponents () {
		foreach (var comp in Selection.activeTransform.GetComponents<Component>())
		{
			if (!(comp is Transform))
			{
				if (!(comp is Animator))
				{
					if (!(comp is Rigidbody))
					{
						if (!(comp is CapsuleCollider))
						{
							if (!(comp is CharController))
							{
								Undo.DestroyObjectImmediate (comp);
							}
						}
					}
				}
			}
		}
	}


    // Validate the menu item defined by the functions above.
    // The menu item will be disabled if this function returns false.
	[MenuItem("Andre's Tools/Player Configurator/Full Config &4", true)]
    static bool ValidateSelectPlayer()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }


	[MenuItem("Andre's Tools/Player Configurator/Remove Components", true)]
	static bool ValidateRemovePlayer()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}

}
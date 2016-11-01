using UnityEditor;
using UnityEngine;
using System.Collections;

public class NPC_Configurator : EditorWindow {
	//Shortcut reminder # -> Shift, & -> alt, % -> ctrl

	[MenuItem ("Andre's Tools/NPC Configurator/Minimal Configuration &1")] //Add a menu item to the toolbar
	static void MinimalNPC () {
		Debug.Log ("Minimal NPC configuration completed!");

		if (!Selection.activeGameObject.GetComponent<CharController> ()) {
			Undo.AddComponent<CharController> (Selection.activeGameObject).enabled = false;
			//Selection.activeGameObject.AddComponent<FSMLogic> ().enabled = false;
		} else {
			Debug.Log ("CharController already added!");
		}

		if (!Selection.activeGameObject.GetComponent<FSMLogic> ()) {
			Undo.AddComponent<FSMLogic> (Selection.activeGameObject).enabled = false;
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


		if (!Selection.activeGameObject.GetComponent<FSM_ReturnInPosition> ()) {
			Undo.AddComponent<FSM_ReturnInPosition> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSM_ReturnInPosition already added!");
		}

		if (!Selection.activeGameObject.GetComponent<NavMeshAgent> ()) {
			Undo.AddComponent<NavMeshAgent> (Selection.activeGameObject).enabled = false;
			Selection.activeGameObject.GetComponent<NavMeshAgent> ().speed = 0.5f;
			Selection.activeGameObject.GetComponent<NavMeshAgent> ().stoppingDistance = 0.2f;

		} else {
			Debug.Log ("NavMeshAgent already added!");
		}
	}


	[MenuItem ("Andre's Tools/NPC Configurator/Standard Configuration &2")] //Add a menu item to the toolbar
	static void StandardNPC () {
		Debug.Log ("Standard NPC configuration completed!");
		//Undo.RecordObject (Selection.activeGameObject, "Standard Configuration");

		if (!Selection.activeGameObject.GetComponent<CharController> ()) {
			Undo.AddComponent<CharController> (Selection.activeGameObject).enabled = false;
			//Selection.activeGameObject.AddComponent<FSMLogic> ().enabled = false;
		} else {
			Debug.Log ("CharController already added!");
		}

		if (!Selection.activeGameObject.GetComponent<FSMLogic> ()) {
			Undo.AddComponent<FSMLogic> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSMLogic already added!");
		}

		if (!Selection.activeGameObject.GetComponent<State_ControlBody> ()) {
			Undo.AddComponent<State_ControlBody> (Selection.activeGameObject);
		} else {
			Debug.Log ("State_ControlBody already added!");
		}

		if (!Selection.activeGameObject.GetComponent<State_PowerControl> ()) {
			Undo.AddComponent<State_PowerControl> (Selection.activeGameObject);
		} else {
			Debug.Log ("State_PowerControl already added!");
		}


		if (!Selection.activeGameObject.GetComponent<FSM_ReturnInPosition> ()) {
			Undo.AddComponent<FSM_ReturnInPosition> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSM_ReturnInPosition already added!");
		}

		if (!Selection.activeGameObject.GetComponent<NavMeshAgent> ()) {
			Undo.AddComponent<NavMeshAgent> (Selection.activeGameObject).enabled = false;
			Selection.activeGameObject.GetComponent<NavMeshAgent> ().speed = 0.5f;
			Selection.activeGameObject.GetComponent<NavMeshAgent> ().stoppingDistance = 0.2f;

		} else {
			Debug.Log ("NavMeshAgent already added!");
		}


		//Per gli NPC che possono essere mandati da qualche parte
		if (!Selection.activeGameObject.GetComponent<FSM_EnemyPath> ()) {
			Undo.AddComponent<FSM_EnemyPath> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSM_EnemyPath already added!");
		}

	}


	[MenuItem ("Andre's Tools/NPC Configurator/Full Configuration &3")] //Add a menu item to the toolbar
	static void FullNPC () {
		Debug.Log ("Full NPC configuration completed!");

		if (!Selection.activeGameObject.GetComponent<CharController> ()) {
			Undo.AddComponent<CharController> (Selection.activeGameObject).enabled = false;
			//Selection.activeGameObject.AddComponent<FSMLogic> ().enabled = false;
		} else {
			Debug.Log ("CharController already added!");
		}


		if (!Selection.activeGameObject.GetComponent<FSMLogic> ()) {
			Undo.AddComponent<FSMLogic> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSMLogic already added!");
		}

		if (!Selection.activeGameObject.GetComponent<State_ControlBody> ()) {
			Undo.AddComponent<State_ControlBody> (Selection.activeGameObject);
		} else {
			Debug.Log ("State_ControlBody already added!");
		}

		if (!Selection.activeGameObject.GetComponent<State_PowerControl> ()) {
			Undo.AddComponent<State_PowerControl> (Selection.activeGameObject);
		} else {
			Debug.Log ("State_PowerControl already added!");
		}


		if (!Selection.activeGameObject.GetComponent<FSM_ReturnInPosition> ()) {
			Undo.AddComponent<FSM_ReturnInPosition> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSM_ReturnInPosition already added!");
		}

		if (!Selection.activeGameObject.GetComponent<NavMeshAgent> ()) {
			Undo.AddComponent<NavMeshAgent> (Selection.activeGameObject).enabled = false;
			Selection.activeGameObject.GetComponent<NavMeshAgent> ().speed = 0.5f;
			Selection.activeGameObject.GetComponent<NavMeshAgent> ().stoppingDistance = 0.2f;

		} else {
			Debug.Log ("NavMeshAgent already added!");
		}


		//Per gli NPC che possono essere mandati da qualche parte
		if (!Selection.activeGameObject.GetComponent<FSM_EnemyPath> ()) {
			Undo.AddComponent<FSM_EnemyPath> (Selection.activeGameObject).enabled = false;
		} else {
			Debug.Log ("FSM_EnemyPath already added!");
		}

		//Per gli NPC che possiedono ricordi
		if (!Selection.activeGameObject.GetComponent<State_ShowMemory> ()) {
			Undo.AddComponent<State_ShowMemory> (Selection.activeGameObject);
		} else {
			Debug.Log ("State_ShowMemory already added!");
		}

		//Per gli NPC a cui si vuole dare accesso a determinate aree
		if (!Selection.activeGameObject.GetComponent<PermissionHandler> ()) {
			Undo.AddComponent<PermissionHandler> (Selection.activeGameObject);
		} else {
			Debug.Log ("PermissionHandler already added!");
		}

	}


	// Validate the menu item defined by the functions above.
	// The menu item will be disabled if this function returns false.
	[MenuItem ("Andre's Tools/NPC Configurator/Standard Configuration &1", true)]
	static bool ValidateSelectTransform1 () {
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}

	[MenuItem ("Andre's Tools/NPC Configurator/Minimal Configuration &2", true)]
	static bool ValidateSelectTransform2 () {
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}

	[MenuItem ("Andre's Tools/NPC Configurator/Full Configuration &3", true)]
	static bool ValidateSelectTransform3 () {
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}


	[MenuItem ("Andre's Tools/NPC Configurator/Remove Components")]
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


}
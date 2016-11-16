using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvents)), CanEditMultipleObjects]
public class PropertyHolderEventEditor : Editor
{

    public SerializedProperty
		condition_Prop,	
		//returnEvent_Prop,
		objectToUse_Prop,
		positionToSpawn_Prop,
		animationName_Prop,
		animationValueFloat_Prop,
		animationValueBool_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
		condition_Prop = serializedObject.FindProperty("whichEvent");
		//returnEvent_Prop = serializedObject.FindProperty("returnEvent");
		objectToUse_Prop = serializedObject.FindProperty("objectToUse");
		positionToSpawn_Prop = serializedObject.FindProperty("positionToSpawn");
		animationName_Prop = serializedObject.FindProperty("animationName");
		animationValueFloat_Prop = serializedObject.FindProperty("animationValueFloat");
		animationValueBool_Prop = serializedObject.FindProperty("animationValueBool");

    }


    public override void OnInspectorGUI()
    {
       	serializedObject.Update();
       	EditorGUILayout.PropertyField(condition_Prop);

		GameEvents.Condition whichEvent = (GameEvents.Condition)condition_Prop.enumValueIndex;
		//Debug.Log ("SPAWNERMYEVENT");

		switch (whichEvent)
        {
		case GameEvents.Condition.Spawner:
			EditorGUILayout.PropertyField(objectToUse_Prop, new GUIContent("objectToUse"));
			EditorGUILayout.PropertyField(positionToSpawn_Prop, new GUIContent("positionToSpawn"));
                break;

		case GameEvents.Condition.PlayAnimationBool:
			EditorGUILayout.PropertyField(objectToUse_Prop, new GUIContent("objectToUse"));
			EditorGUILayout.PropertyField(animationName_Prop, new GUIContent("animationName"));
			EditorGUILayout.PropertyField(animationValueBool_Prop, new GUIContent("animationValueBool"));
                break;
          
		case GameEvents.Condition.PlayAnimationFloat:                
			EditorGUILayout.PropertyField(objectToUse_Prop, new GUIContent("objectToUse"));
			EditorGUILayout.PropertyField(animationName_Prop, new GUIContent("animationName"));
			EditorGUILayout.PropertyField(animationValueFloat_Prop, new GUIContent("animationValueFloat"));
                break;
            
        }

		//EditorGUILayout.PropertyField(returnEvent_Prop);

        serializedObject.ApplyModifiedProperties();
    }
}

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
        int_Prop,
        sequenceName_Prop,
        positionArray_Prop,
        patrolingTransArray_Prop,
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
        int_Prop = serializedObject.FindProperty("i");
        positionArray_Prop = serializedObject.FindProperty("positionArray");
        sequenceName_Prop = serializedObject.FindProperty("sequenceName");
        animationValueBool_Prop = serializedObject.FindProperty("animationValueBool");
        patrolingTransArray_Prop = serializedObject.FindProperty("patrolingTrans");

    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(condition_Prop);
        //EditorGUILayout.PropertyField(int_Prop);
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

            case GameEvents.Condition.RandomActionSequence:
                EditorGUILayout.PropertyField(sequenceName_Prop, new GUIContent("sequenceName"));
                EditorGUILayout.PropertyField(positionArray_Prop, new GUIContent("positionArray"));                
                break;
            case GameEvents.Condition.ActionSequence:
                EditorGUILayout.PropertyField(sequenceName_Prop, new GUIContent("sequenceName"));
                EditorGUILayout.PropertyField(positionArray_Prop, new GUIContent("positionArray"));
                break;
            case GameEvents.Condition.Patroling:
                EditorGUILayout.PropertyField(patrolingTransArray_Prop);
                if (patrolingTransArray_Prop.isExpanded)
                {                   
                    EditorGUILayout.PropertyField(patrolingTransArray_Prop.FindPropertyRelative("Array.size"));

                }

                break;
        }


        //EditorGUILayout.PropertyField(returnEvent_Prop);

        serializedObject.ApplyModifiedProperties();
    }
}

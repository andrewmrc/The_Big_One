using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvents)), CanEditMultipleObjects]
public class PropertyHolderEventEditor : Editor
{
    string[] nameRandomSequence;
    string[] nameSequence;
    string[] nAction;

    public SerializedProperty


        condition_Prop,
        //returnEvent_Prop,
        objectToUse_Prop,
        positionToSpawn_Prop,
        animationName_Prop,
        animationValueFloat_Prop,
        int_Prop,
        sequenceName_Prop,
        sceneName_Prop,
        positionArray_Prop,
        patrolingTransArray_Prop,
        patrolingObj_Prop,
        patrolingSpeed_Prop,
        patrolingStunWait_Prop,
        isSequenceRandom_Prop,
        isSequence_Prop,
        choice_Prop,
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
		sceneName_Prop = serializedObject.FindProperty("sceneName");
        animationValueBool_Prop = serializedObject.FindProperty("animationValueBool");
        patrolingObj_Prop = serializedObject.FindProperty("patrolingObj");
        patrolingSpeed_Prop = serializedObject.FindProperty("speedObj");
        patrolingStunWait_Prop = serializedObject.FindProperty("stunWaiting");
        patrolingTransArray_Prop = serializedObject.FindProperty("moveTransform");
        isSequenceRandom_Prop = serializedObject.FindProperty("isSequenceRandom");
        choice_Prop = serializedObject.FindProperty("choice");

        nameRandomSequence = new string[FlowManager.Self.flowRandomGameArray.Length];
        nameSequence = new string[FlowManager.Self.flowGameArray.Count];

        for (int i = 0; i < FlowManager.Self.flowGameArray.Count; i++)
        {
            nameSequence[i] = FlowManager.Self.flowGameArray[i].SequenceName;
        }

        for (int i = 0; i < FlowManager.Self.flowRandomGameArray.Length; i++)
        {
            nameRandomSequence[i] = FlowManager.Self.flowRandomGameArray[i].SequenceName;
        }

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
                EditorGUILayout.PropertyField(isSequenceRandom_Prop, new GUIContent("Sequenza Random"));
                if (isSequenceRandom_Prop.boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Nome Azione");
                    choice_Prop.intValue = EditorGUILayout.Popup(choice_Prop.intValue,nameRandomSequence);
                    EditorGUILayout.EndHorizontal();
                }
                break;

            case GameEvents.Condition.PlayAnimationBool:
                EditorGUILayout.PropertyField(objectToUse_Prop, new GUIContent("objectToUse"));
                EditorGUILayout.PropertyField(animationName_Prop, new GUIContent("animationName"));
                EditorGUILayout.PropertyField(animationValueBool_Prop, new GUIContent("animationValueBool"));
                EditorGUILayout.PropertyField(isSequenceRandom_Prop, new GUIContent("Sequenza Random"));
                if (isSequenceRandom_Prop.boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Nome Azione");
                    choice_Prop.intValue = EditorGUILayout.Popup(choice_Prop.intValue, nameRandomSequence);
                    EditorGUILayout.EndHorizontal();
                }
                break;

            case GameEvents.Condition.PlayAnimationFloat:
                EditorGUILayout.PropertyField(objectToUse_Prop, new GUIContent("objectToUse"));
                EditorGUILayout.PropertyField(animationName_Prop, new GUIContent("animationName"));
                EditorGUILayout.PropertyField(animationValueFloat_Prop, new GUIContent("animationValueFloat"));
                EditorGUILayout.PropertyField(isSequenceRandom_Prop, new GUIContent("Sequenza Random"));
                if (isSequenceRandom_Prop.boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Nome Azione");
                    choice_Prop.intValue = EditorGUILayout.Popup(choice_Prop.intValue, nameRandomSequence);
                    EditorGUILayout.EndHorizontal();
                }
                break;

            case GameEvents.Condition.RandomActionSequence:
                EditorGUILayout.PropertyField(sequenceName_Prop, new GUIContent("sequenceName"));
                EditorGUILayout.PropertyField(positionArray_Prop, new GUIContent("positionArray"));                
                break;

            case GameEvents.Condition.ActionSequence:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Nome Azione");
                choice_Prop.intValue = EditorGUILayout.Popup(choice_Prop.intValue, nameSequence);
                EditorGUILayout.EndHorizontal();
                // inizializzo l'array con il numero di 
                nAction = new string[FlowManager.Self.flowGameArray[choice_Prop.intValue].sequence.Length];
                for (int i = 0; i < nAction.Length; i++)
                {
                    nAction[i] = (i + 1).ToString();
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Posizione");
                positionArray_Prop.intValue = EditorGUILayout.Popup(positionArray_Prop.intValue, nAction);
                EditorGUILayout.EndHorizontal();
                break;

			case GameEvents.Condition.LoadScene:
				EditorGUILayout.PropertyField(sceneName_Prop, new GUIContent("sceneName"));
				
				break;

			case GameEvents.Condition.Patrolling:
                EditorGUILayout.PropertyField(patrolingTransArray_Prop);
                
                if (patrolingTransArray_Prop.isExpanded)
                {
                    EditorGUILayout.PropertyField(patrolingTransArray_Prop.FindPropertyRelative("Array.size"));
                    EditorGUI.indentLevel += 1;

                    

                    for (int i = 0; i < patrolingTransArray_Prop.arraySize; i++)
                    {
                        EditorGUILayout.PropertyField(patrolingTransArray_Prop.GetArrayElementAtIndex(i));
                        if (patrolingTransArray_Prop.GetArrayElementAtIndex(i).isExpanded)
                        {
                            SerializedProperty target = patrolingTransArray_Prop.GetArrayElementAtIndex(i).FindPropertyRelative("target");
                            SerializedProperty delay = patrolingTransArray_Prop.GetArrayElementAtIndex(i).FindPropertyRelative("delay");
                            EditorGUILayout.PropertyField(target);
                            EditorGUILayout.PropertyField(delay);
                        }
                        
                    }
                    EditorGUI.indentLevel -= 1;
                    
                }
                EditorGUILayout.PropertyField(patrolingObj_Prop);
                EditorGUILayout.PropertyField(patrolingSpeed_Prop);
                EditorGUILayout.PropertyField(patrolingStunWait_Prop);
                EditorGUILayout.PropertyField(isSequenceRandom_Prop, new GUIContent("Sequenza Random"));
                if (isSequenceRandom_Prop.boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Nome Azione");
                    choice_Prop.intValue = EditorGUILayout.Popup(choice_Prop.intValue, nameRandomSequence);
                    EditorGUILayout.EndHorizontal();
                }
                break;
        }


        //EditorGUILayout.PropertyField(returnEvent_Prop);

        serializedObject.ApplyModifiedProperties();
    }
}

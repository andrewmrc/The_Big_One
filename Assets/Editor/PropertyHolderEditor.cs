using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerAction)), CanEditMultipleObjects]
public class PropertyHolderEditor : Editor
{

    public SerializedProperty
        condition_Prop,
        triggerEnterEvent_Prop,
        triggerExitEvent_Prop,
        triggerStayEvent_Prop,
        controllable_Prop;
        

    void OnEnable()
    {
		
        // Setup the SerializedProperties
        condition_Prop = serializedObject.FindProperty("wichCondition");
        triggerEnterEvent_Prop = serializedObject.FindProperty("triggerEnterEvent");
        triggerExitEvent_Prop = serializedObject.FindProperty("triggerExit");
        triggerStayEvent_Prop = serializedObject.FindProperty("triggerStay");
        controllable_Prop = serializedObject.FindProperty("controllable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(condition_Prop);
        TriggerAction.Condition wichCondition = (TriggerAction.Condition)condition_Prop.enumValueIndex;
        
        switch (wichCondition)
        {
            case TriggerAction.Condition.triggerEnter:
                EditorGUILayout.PropertyField(triggerEnterEvent_Prop, new GUIContent("triggerEnterEvent"));
                break;
            case TriggerAction.Condition.triggerExit:
                EditorGUILayout.PropertyField(triggerExitEvent_Prop, new GUIContent("triggerExit"));
                break;
            case TriggerAction.Condition.triggerStay:                
                EditorGUILayout.PropertyField(triggerStayEvent_Prop, new GUIContent("triggerStay"));
                break;            
        }


        serializedObject.ApplyModifiedProperties();
    }
}

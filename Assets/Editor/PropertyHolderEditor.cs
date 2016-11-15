using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProvaFlow)), CanEditMultipleObjects]
public class PropertyHolderEditor : Editor
{

    public SerializedProperty
        condition_Prop,
        triggerEnterEvent_Prop,
        triggerExitEvent_Prop,
        triggerStayEvent_Prop,
        controllable_Prop,
        positionArray_Prop,
        secondPosition_Prop;
        

    void OnEnable()
    {
		
        // Setup the SerializedProperties
        condition_Prop = serializedObject.FindProperty("wichCondition");
        triggerEnterEvent_Prop = serializedObject.FindProperty("triggerEnterEvent");
        triggerExitEvent_Prop = serializedObject.FindProperty("triggerExit");
        triggerStayEvent_Prop = serializedObject.FindProperty("triggerStay");
        controllable_Prop = serializedObject.FindProperty("controllable");
        positionArray_Prop = serializedObject.FindProperty("positionInFlowArray");
        secondPosition_Prop = serializedObject.FindProperty("secondPosition");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(condition_Prop);
        EditorGUILayout.PropertyField(positionArray_Prop);
        ProvaFlow.Condition wichCondition = (ProvaFlow.Condition)condition_Prop.enumValueIndex;

        EditorGUILayout.PropertyField(secondPosition_Prop);
        


        
        switch (wichCondition)
        {
            case ProvaFlow.Condition.triggerEnter:
                EditorGUILayout.PropertyField(triggerEnterEvent_Prop, new GUIContent("triggerEnterEvent"));
                //EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                //EditorGUILayout.IntSlider(valForA_Prop, 0, 10, new GUIContent("valForA"));
                //EditorGUILayout.IntSlider(valForAB_Prop, 0, 100, new GUIContent("valForAB"));
                break;
            case ProvaFlow.Condition.triggerExit:
                EditorGUILayout.PropertyField(triggerExitEvent_Prop, new GUIContent("triggerExit"));
                break;
            /*case PropertyHolder.Status.B:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForAB_Prop, 0, 100, new GUIContent("valForAB"));
                break;

            case PropertyHolder.Status.C:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForC_Prop, 0, 100, new GUIContent("valForC"));
                break;*/
            case ProvaFlow.Condition.triggerStay:                
                EditorGUILayout.PropertyField(triggerStayEvent_Prop, new GUIContent("triggerStay"));
                break;
            
        }


        serializedObject.ApplyModifiedProperties();
    }
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProvaFlow)), CanEditMultipleObjects]
public class PropertyHolderEditor : Editor
{

    public SerializedProperty
        condition_Prop,
        newEvent_Prop,
        valForA_Prop,
        valForC_Prop,
        controllable_Prop,
        positionArray_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        condition_Prop = serializedObject.FindProperty("wichCondition");
        newEvent_Prop = serializedObject.FindProperty("triggerEvent");
        valForA_Prop = serializedObject.FindProperty("isTrigger");
        valForC_Prop = serializedObject.FindProperty("quelloCheVuoiEvent");
        controllable_Prop = serializedObject.FindProperty("controllable");
        positionArray_Prop = serializedObject.FindProperty("positionInFlowArray");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(condition_Prop);

        ProvaFlow.Condition wichCondition = (ProvaFlow.Condition)condition_Prop.enumValueIndex;

        EditorGUILayout.PropertyField(positionArray_Prop);
        switch (wichCondition)
        {
            case ProvaFlow.Condition.isConversation:
                EditorGUILayout.PropertyField(newEvent_Prop, new GUIContent("isTrigger"));
                //EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                //EditorGUILayout.IntSlider(valForA_Prop, 0, 10, new GUIContent("valForA"));
                //EditorGUILayout.IntSlider(valForAB_Prop, 0, 100, new GUIContent("valForAB"));
                break;
            case ProvaFlow.Condition.isTrigger:
                break;
            /*case PropertyHolder.Status.B:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForAB_Prop, 0, 100, new GUIContent("valForAB"));
                break;

            case PropertyHolder.Status.C:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForC_Prop, 0, 100, new GUIContent("valForC"));
                break;*/
            case ProvaFlow.Condition.quellochevuoi:
                EditorGUILayout.PropertyField(valForC_Prop, new GUIContent("quelloCheVuoiEvent"));
                break;
            case ProvaFlow.Condition.nothing:
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}

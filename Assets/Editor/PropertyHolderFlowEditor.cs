using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlowManager)), CanEditMultipleObjects]
public class PropertyHolderFlowEditor : Editor
{
    bool showPosition = true;
    string status = "Select a GameObject";
    //FlowManager t;
    SerializedObject GetTarget;
    public SerializedProperty
        randomArray_Prop,
        sequenceArray_Prop,
        asd;

    //returnEvent_Prop,


    void OnEnable()
    {
        // Setup the SerializedProperties
        //t = (FlowManager)target;

        randomArray_Prop = serializedObject.FindProperty("flowRandomGameArray");
        sequenceArray_Prop = serializedObject.FindProperty("flowGameArray");
        asd = serializedObject.FindProperty("asd");
        

    }



    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Show(serializedObject.FindProperty("flowRandomGameArray"));
        Show(serializedObject.FindProperty("flowGameArray"));
        serializedObject.ApplyModifiedProperties();

    }

    void Show(SerializedProperty list)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(list);
        EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel += 1;

        if (list.isExpanded)
        {

            for (int i = 0; i < list.arraySize; i++)
            {

                SerializedProperty MyListRef = list.GetArrayElementAtIndex(i);
                
                
                EditorGUILayout.PropertyField(MyListRef);
                if (MyListRef.isExpanded)
                {                    
                    


                    SerializedProperty callAction = MyListRef.FindPropertyRelative("call");
                    SerializedProperty arrayBool = MyListRef.FindPropertyRelative("sequence");
                    SerializedProperty sequenceName = MyListRef.FindPropertyRelative("SequenceName");
                    status = sequenceName.stringValue;
                    sequenceName.stringValue = EditorGUILayout.TextField("Nome ", sequenceName.stringValue);
                    EditorGUILayout.PropertyField(callAction);
                    EditorGUILayout.BeginHorizontal();
                    arrayBool.arraySize = EditorGUILayout.IntSlider("N sequenze ", arrayBool.arraySize, 1, 10);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    for (int a = 0; a < arrayBool.arraySize; a++)
                    {
                        arrayBool.GetArrayElementAtIndex(a).boolValue = EditorGUILayout.Toggle(arrayBool.GetArrayElementAtIndex(a).boolValue);
                        
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
            }

        }
        EditorGUI.indentLevel -= 1;


    }
}

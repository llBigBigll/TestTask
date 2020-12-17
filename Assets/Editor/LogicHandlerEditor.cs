using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(LogicHandler))]
public class LogicHandlerEditor : Editor
{
    private ReorderableList viewlist;

    public void OnEnable()
    {
        viewlist = new ReorderableList(serializedObject, serializedObject.FindProperty("stringsList"),
       false, true, true, true);

        viewlist.drawElementCallback = new ReorderableList.ElementCallbackDelegate(onRedrawElements);
        viewlist.drawHeaderCallback = new ReorderableList.HeaderCallbackDelegate(onRedrawHeader);
    }

    public override void OnInspectorGUI()
    {
        
        LogicHandler script = (LogicHandler)target;
        EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ContentHolder: ");
            script.listHolder = (GameObject)EditorGUILayout.ObjectField(script.listHolder,typeof(Object),true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Row Prefab: ");
            script.listElementPref = (GameObject)EditorGUILayout.ObjectField(script.listElementPref, typeof(Object), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("InputField: ");
            script.textField = (InputField)EditorGUILayout.ObjectField(script.textField, typeof(InputField), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("AddButton: ");
            script.bttnAdd = (Button)EditorGUILayout.ObjectField(script.bttnAdd, typeof(Button), true);
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        serializedObject.Update();
        viewlist.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

    }

    private void onRedrawElements(Rect rect, int index, bool isActive, bool isFocused) 
    {
        var element = viewlist.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
            element, GUIContent.none);
    }

    private void onRedrawHeader(Rect rect) 
    {
        EditorGUI.LabelField(rect, "List of strings");
    }
}

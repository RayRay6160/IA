using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SFX), true)]
public class SFXEditor : Editor {

    [SerializeField] AudioSource _previewer;

    void OnEnable() {
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview",HideFlags.HideAndDontSave,typeof(AudioSource)).GetComponent<AudioSource>();
    }

    void OnDisable() {
        DestroyImmediate(_previewer);
    }

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);

        if (GUILayout.Button("Preview")) {
            ((SFX)target).Play(_previewer);
        }

        EditorGUI.EndDisabledGroup();

    }

}
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Debug))]
public class DebugEditor : Editor {
    // ----------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear")) {
            Debug d = (Debug)target;
                
            d.example = false;
        }
    }
}
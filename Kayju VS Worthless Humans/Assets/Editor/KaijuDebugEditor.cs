using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(KaijuDebug))]
public class KaijuDebugEditor : Editor {
    // ----------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear")) {
            KaijuDebug d = (KaijuDebug)target;
                
            d.example = false;
        }
    }
}
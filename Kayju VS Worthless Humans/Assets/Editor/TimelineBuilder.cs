using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Timeline))]
public class TimelineBuilder : Editor {
    private int methodIndex = 0;

    // ----------------------------------------------------------------------------
    // Public Methods
    // ----------------------------------------------------------------------------

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        List<MonoBehaviour> scripts = new List<MonoBehaviour>();
        List<string> methods = new List<string>();

        Timeline timeline = (Timeline)target;
        foreach (MonoBehaviour script in timeline.gameObject.GetComponents<MonoBehaviour>()) {
            // Get all methods
            MethodInfo[] scriptMethods = script.GetType().GetMethods();

            foreach (MethodInfo method in scriptMethods) {
                // Discard all methods not having the TimelineMethod attribute
                if (method.GetCustomAttributes(typeof(TimelineMethod), true).Length > 0) {
                    scripts.Add(script);
                    methods.Add(method.Name);
                }
            }
        }

        // Create list options
        string[] options = new string[methods.Count];
        for (int i = 0; i < methods.Count; i++) {
            options[i] = methods[i] + " (" + scripts[i].GetType().Name + ")";
        }

        // Init timeline
        if (timeline.actions == null) {
            timeline.actions = new List<TimelineAction>();
        }

        // Title
        GUILayout.Label("Timeline actions");

        for (int i = 0; i < timeline.actions.Count; i++) {
            TimelineAction action = timeline.actions[i];

            // New row
            GUILayout.BeginHorizontal();

            // Method
            methodIndex = EditorGUILayout.Popup(methods.IndexOf(action.method), options);
            if (GUI.changed) {
                action.method = methods[methodIndex];

                GUI.changed = false;
            }

            // Parameter
            action.parameter = EditorGUILayout.IntField(action.parameter, GUILayout.Width(30));

            // From
            GUILayout.Label("from", GUILayout.Width(30));
            action.from = EditorGUILayout.FloatField(action.from, GUILayout.Width(30));

            // To
            GUILayout.Label("to", GUILayout.Width(15));
            action.to = EditorGUILayout.FloatField(action.to, GUILayout.Width(30));

            // Remove the row
            if (GUILayout.Button("-", GUILayout.Width(25))) {
                timeline.actions.RemoveAt(i);
            }

            // Move down
            if (GUILayout.Button("v", GUILayout.Width(25))) {
                if (i < timeline.actions.Count - 1) {
                    timeline.actions.RemoveAt(i);
                    timeline.actions.Insert(i + 1, action);
                }
            }

            // Move up
            if (GUILayout.Button("^", GUILayout.Width(25))) {
                if (i > 0) {
                    timeline.actions.RemoveAt(i);
                    timeline.actions.Insert(i - 1, action);
                }
            }

            GUILayout.EndHorizontal();
        }

        // Add a row
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+")) {
            if (methods.Count == 0) {
                UnityEngine.Debug.LogWarning("No Timeline Method found");
            } else {
                timeline.actions.Add(new TimelineAction(scripts[0], methods[0]));
            }
        }
        GUILayout.EndHorizontal();

        EditorUtility.SetDirty(target);
    }
}

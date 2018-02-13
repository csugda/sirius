﻿using Assets.Scripts.AI;
using Assets.Scripts.AI.Components;
using BehaviorTreeViewEditor.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviorManager))]
public class BehaviorManagerEditor : Editor
{
    SerializedProperty runner;
    SerializedProperty runnerBehaviors;

    BehaviorManager manager;

    void OnEnable()
    {
        //has to be initialized
        ((BehaviorManager)serializedObject.targetObject).Init();
        runner = serializedObject.FindProperty("Runner");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); 
        runnerBehaviors = runner.FindPropertyRelative("SubBehaviors");
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.PropertyField(runner);

            if (GUILayout.Button("Edit Tree"))
            {
                MultiColumnBTreeWindow btreeWindow = CreateInstance<MultiColumnBTreeWindow>();
                btreeWindow.maximized = false;
                btreeWindow.Show();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

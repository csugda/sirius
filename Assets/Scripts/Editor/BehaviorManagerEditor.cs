using Assets.Scripts.AI;
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
    SerializedProperty asset;

    BehaviorManager manager;

    string TreeName = "NewBehavior";

    void OnEnable()
    {
        ((BehaviorManager)serializedObject.targetObject).Init();
        //has to be initialized
        runner = serializedObject.FindProperty("Runner");
        asset = serializedObject.FindProperty("BehaviorTree");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        runnerBehaviors = runner.FindPropertyRelative("_Children");

        EditorGUILayout.PropertyField(asset);

        var Asset = asset.objectReferenceValue as BehaviorTreeAsset;

        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.PropertyField(runner);

            if (((BehaviorManager)serializedObject.targetObject).BehaviorTree == null)
            {
                TreeName = EditorGUILayout.TextField(TreeName);
                if (GUILayout.Button("Create New Tree"))
                {
                    Asset = CreateInstance<BehaviorTreeAsset>();
                    AssetDatabase.CreateAsset(Asset, "Assets/Scripts/AI/BehaviorTrees/" + TreeName + ".asset");
                    AssetDatabase.Refresh();
                    ((BehaviorManager)serializedObject.targetObject).BehaviorTree = Asset;
                    ((BehaviorManager)serializedObject.targetObject).LoadTree();
                    EditorUtility.SetDirty(Asset);
                }
            }
            else
            {
                if (GUILayout.Button("Load/Reload Tree"))
                {
                    AssetDatabase.Refresh();
                    ((BehaviorManager)serializedObject.targetObject).BehaviorTree = Asset;
                    ((BehaviorManager)serializedObject.targetObject).LoadTree();
                    EditorUtility.SetDirty(Asset);
                }
                if (GUILayout.Button("Edit Tree"))
                {
                    MultiColumnBTreeWindow btreeWindow = MultiColumnBTreeWindow.GetWindow();
                    btreeWindow.SetTreeAsset(Asset);
                    btreeWindow.maximized = false;
                    btreeWindow.Show();
                }
                if (GUILayout.Button("Save Tree"))
                {
                    AssetDatabase.SaveAssets();
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}

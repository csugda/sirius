using UnityEngine;
using UnityEditor;
using BehaviorTreeViewEditor.BackendData;

[CustomEditor(typeof(BehaviorTreeScript),true)]
public class BehaviorTreeScriptEditor : Editor
{
    SerializedProperty behaviorTree;
    int test = 0;

    void OnEnable()
    {
        behaviorTree = serializedObject.FindProperty("tree");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (new EditorGUILayout.HorizontalScope())
        {
            if(GUILayout.Button("Edit Tree"))
            {
                MultiColumnBTreeWindow btreeWindow = ScriptableObject.CreateInstance<MultiColumnBTreeWindow>();
                btreeWindow.maximized = false;
                btreeWindow.Show();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

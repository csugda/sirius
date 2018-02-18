using Assets.Scripts.AI;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviorManager))]
public class BehaviorManagerEditor : Editor
{
    SerializedProperty assets;
    SerializedProperty SecondsBetweenTicks;
    SerializedProperty TimesToTick;

    BehaviorManager manager;

    void OnEnable()
    {
        ((BehaviorManager)serializedObject.targetObject).Init();
        //has to be initialized
        assets = serializedObject.FindProperty("BehaviorTrees");
        TimesToTick = serializedObject.FindProperty("TimesToTick");
        SecondsBetweenTicks = serializedObject.FindProperty("SecondsBetweenTicks");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //runnerBehaviors = runner.FindPropertyRelative("children");

        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.PropertyField(TimesToTick);
            EditorGUILayout.PropertyField(SecondsBetweenTicks);
            var btrees = ((BehaviorManager)serializedObject.targetObject).BehaviorTrees;
            if (btrees == null || btrees.Count <= 0)
            {

                if (GUILayout.Button("Create New Tree"))
                {
                    CustomAssetUtility.CreateAsset<BehaviorTreeAsset>();
                    var Asset = (BehaviorTreeAsset)Selection.activeObject;

                    ((BehaviorManager)serializedObject.targetObject).BehaviorTrees.Add(Asset);
                    ((BehaviorManager)serializedObject.targetObject).LoadTree();
                }
            }
            else
            {
                EditorList.Show(assets, EditorListOption.Buttons);
                if (GUILayout.Button("Load/Reload Trees"))
                {
                    AssetDatabase.Refresh();
                    ((BehaviorManager)serializedObject.targetObject).BehaviorTrees = btrees;
                    ((BehaviorManager)serializedObject.targetObject).LoadTree();

                    foreach(var behavior in btrees)
                    {
                        EditorUtility.SetDirty(behavior);
                    } 
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}

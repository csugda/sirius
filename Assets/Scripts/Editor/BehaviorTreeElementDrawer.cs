using Assets.Scripts.AI;
using Assets.Scripts.AI.Components;
using BehaviorTreeViewEditor.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BehaviorTreeElement), true)]
public class BehaviorTreeElementDrawer : PropertyDrawer
{
    GUIStyle style = new GUIStyle();

    private Color GetBehaviorStateColor(int state)
    {
        switch(state)
        {
            case (int)BehaviorState.Fail:
                return Color.red;
            case (int)BehaviorState.Running:
                return Color.blue;
            case (int)BehaviorState.Success:
                return new Color(0.1f,0.9f,0.2f);
            case (int)BehaviorState.Null:
                return Color.grey;
            default:
                return Color.black;
        }
    }

    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginProperty(pos, label, prop);

        var nameRect = new Rect(pos.x - 5, pos.y, 2 * pos.width / 3, pos.height);
        var managerRect = new Rect(pos.x, pos.y, pos.width, pos.height);

        var behaviorState = prop.FindPropertyRelative("_CurrentState");

        if(behaviorState != null)
        {
            style.onNormal.textColor = GetBehaviorStateColor(behaviorState.intValue);
            style.normal.textColor = GetBehaviorStateColor(behaviorState.intValue);
        }
        else
        {
            style.onNormal.textColor = Color.black;
            style.normal.textColor = Color.black;
        }

        EditorGUI.LabelField(nameRect, "Name: " + prop.FindPropertyRelative("_Name").stringValue, style);

        var subs = prop.FindPropertyRelative("SubBehaviors");
        if(subs != null)
        {
            EditorGUI.indentLevel += 1;
            EditorList.Show(subs, EditorListOption.Buttons);
            EditorGUI.indentLevel -= 1;
        }


        EditorGUI.EndProperty();
    }
}

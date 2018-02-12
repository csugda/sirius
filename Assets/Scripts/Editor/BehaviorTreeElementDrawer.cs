using Assets.Scripts.AI;
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
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginProperty(pos, label, prop);
        var nameRect = new Rect(pos.x, pos.y, 2*pos.width/3, pos.height);
        var IDRect = new Rect(pos.x+nameRect.width+5, pos.y, pos.width/3, pos.height);
        var managerRect = new Rect(pos.x, pos.y, pos.width, pos.height);
        EditorGUI.PropertyField(nameRect, prop.FindPropertyRelative("_Name"), new GUIContent("Name"));
        EditorGUI.LabelField(IDRect, prop.FindPropertyRelative("_ID").intValue.ToString());
        EditorGUI.EndProperty();
    }
}

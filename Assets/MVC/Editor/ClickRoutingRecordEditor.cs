using UnityEditor;
using MVC;
using UnityEngine.UIElements;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(ClickRoutingRecord))]
public class ClickRoutingRecordEditor : Editor
{
    private int _targetIndex = -1;
    private string[] _targets = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _targets = LoadTargets();
        _targetIndex = EditorGUILayout.Popup(_targetIndex, _targets);

        if (_targetIndex != -1)
        {
            serializedObject.FindProperty("_target").stringValue = _targets[_targetIndex];
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string[] LoadTargets()
    {
        var methods = Assembly.GetAssembly(typeof(MVCEventAttribute)).GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(MVCEventAttribute), false).Length > 0)
                .ToArray();

        var targets = new List<string>();

        foreach (var method in methods)
            targets.Add($"{method.Name}:{method.DeclaringType.FullName}"); //очень плохой формат

        return targets.ToArray();
    }
}
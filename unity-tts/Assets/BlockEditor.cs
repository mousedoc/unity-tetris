using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
    private SerializedProperty instance;

    public Block Block { get { return target as Block; } }

    private void OnEnable()
    {
        instance = serializedObject.FindProperty("instance;");
    }

    public override void OnInspectorGUI()
    {
        var list = new List<string>
        {
            string.Format("Color : {0}", Block.Color.ToString()),
            string.Format("IsActive : {0}", Block.IsActive.ToString()),
            string.Format("IsGuide : {0}", Block.IsGuide.ToString()),
        };

        foreach (var label in list)
            EditorGUILayout.LabelField(label);
    }
}

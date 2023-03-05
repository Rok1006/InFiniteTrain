using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEditor;

[CustomEditor(typeof(InventorySlotPlus))]
public class InventorySlotEditorPlus : InventorySlotEditor
{
    protected SerializedProperty _tooltip;
    protected SerializedProperty _tooltipText;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        _tooltip = serializedObject.FindProperty("tooltip");
        _tooltipText = serializedObject.FindProperty("tooltipText");
    }

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(_tooltip);
        EditorGUILayout.PropertyField(_tooltipText);

        serializedObject.ApplyModifiedProperties();

    }
}

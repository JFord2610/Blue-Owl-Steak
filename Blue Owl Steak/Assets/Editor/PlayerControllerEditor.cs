using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Deal 10 damage to player"))
        {
            ((PlayerController)target).TakeDamage(10);
        }
        if(GUILayout.Button("Kill Player"))
        {
            ((PlayerController)target).TakeDamage(10000);
        }
    }
}

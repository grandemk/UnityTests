using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelScript))]
public class LevelScriptEditor : Editor 
{
    [MenuItem("Tools/Opt1 _g", false, 1)]
    private static void MenuOption()
    {
        Debug.Log("Menu Option 1 clicked");
    }

    [MenuItem("Tools/Opt2", false, 2)]
    private static void MenuOption2()
    {
        Debug.Log("Menu Option 2 clicked");
    }

    // Menu items are automatically grouped according to their assigned priority in increments of 50.
    [MenuItem("Tools/Opt3", false, 50)]
    private static void MenuOption3()
    {
        Debug.Log("Menu Option 3 clicked");
    }

    [MenuItem("CONTEXT/LevelScript/Coucou")]
    private static void ContextStuff(MenuCommand menuCommand)
    {
        var lvl = menuCommand.context as LevelScript;
        lvl.experience += 100000;
        Debug.Log("Boum " + lvl.experience + "XP!");
    }

    [MenuItem("Assets/CoucouAsset")]
    private static void CoucouAsset()
    {
        Debug.Log("Coucou asset !");
    }

    // Validation function for the menu
    [MenuItem("Assets/CoucouAsset", true)]
    private static bool ValidateCoucouAsset()
    {
        return false;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Do stuff"))
        {
            Debug.Log("Did stuff");
        }
        GUILayout.Label("Coucou");
        LevelScript myTarget = (LevelScript)target;
        myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}

using UnityEditor;
using UnityEngine;

public class InternalToolkit : EditorWindow
{
    [MenuItem("Window/Pipo's Tools/Toolkit")]
    public static void ShowWindow()
    {
        GetWindow<InternalToolkit>("Toolkit");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Game Toolkit", EditorStyles.boldLabel);

        if (GUILayout.Button("Make Blue"))
        {

        }
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Make Red"))
        {

        }

        if (GUILayout.Button("Make Green"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        GUILayout.Label("Spawning", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Spawn Sphere"))
        {

        }

        if (GUILayout.Button("Spawn Cube"))
        {

        }
        GUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
    }
}

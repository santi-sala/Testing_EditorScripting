using UnityEditor;
using UnityEngine;

public class InternalToolkit : EditorWindow
{
    [MenuItem("Window/Pipo's Tools/Toolkit")]
    public static void ShowWindow()
    {
        GetWindow<InternalToolkit>("Toolkit");
    }

    private GameObject _currentSelectedGameObject;

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Game Toolkit", EditorStyles.boldLabel);

        if (GUILayout.Button("Make Blue"))
        {
            SetColor(Color.blue);
        }
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Make Red"))
        {
            SetColor(Color.red);
        }

        if (GUILayout.Button("Make Green"))
        {
            SetColor(Color.green);
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
    private void OnFocus()
    {
        _currentSelectedGameObject = Selection.activeGameObject;
    }

    private void OnLostFocus()
    {
        _currentSelectedGameObject = null;
    }

    private void SetColor(Color selectedColor)
    {
        if(_currentSelectedGameObject != null)
        {
            Renderer renderer = _currentSelectedGameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial.color = selectedColor;
            }
        }
    }
}

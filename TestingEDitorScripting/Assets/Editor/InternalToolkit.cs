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
        
    }
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogGenerator))]
public class DialogGeneratorEditor : Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        var script = (DialogGenerator) target;
        if (GUILayout.Button("Gerar Json")){
            script.GenerateJsonFile();
        }
    }
}
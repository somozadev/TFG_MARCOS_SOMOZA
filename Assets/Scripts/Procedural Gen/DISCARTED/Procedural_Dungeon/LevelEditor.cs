using UnityEditor;
using UnityEngine;


    [CustomEditor(typeof(Level))]
    public class GeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Level myScript = (Level)target;
            if (GUILayout.Button("Add Section Template"))
            {
                myScript.AddSectionTemplate();
            }

            if (GUILayout.Button("Add Dead End Template"))
            {
                myScript.AddDeadEndTemplate();
            }
        }
    }


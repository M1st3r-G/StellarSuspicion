using Controller;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(WindowController))]
    internal class WindowEditor : UnityEditor.Editor {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Label("");
            GUILayout.Label("Dev Tools", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Open/Close Window (Runtime/Editor)"))
            {
                WindowController tmp = (WindowController)target;
                tmp.SetWindowOpened(!tmp.IsOpen);
            }
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(WindowController))]
    class WindowEditor : UnityEditor.Editor {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Label("");
            GUILayout.Label("Dev Tools", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Open/Close Window"))
            {
                WindowController tmp = (WindowController)target;
                tmp.SetWindowOpened(!tmp.IsOpen);
            }
        }
    }
}
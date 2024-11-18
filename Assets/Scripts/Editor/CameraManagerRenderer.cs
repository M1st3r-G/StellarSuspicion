using Manager;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CameraManagerRenderer))]
    public class CameraManagerRenderer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label("Dev Tools", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Go to Sitting Camera (Runtime)"))
            {
                CameraManager.SwitchCamera(true);
            }

            if (GUILayout.Button("Go to Player Camera (Runtime)"))
            {
                CameraManager.SwitchCamera(false);
            }
        }
    }
}
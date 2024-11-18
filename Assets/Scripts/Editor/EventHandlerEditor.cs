using Manager;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EventHandler))]
    public class EventHandlerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label("Dev Tools", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Trigger Random Event (Runtime)"))
            {
                EventHandler tmp = (EventHandler)target;
                tmp.TriggerRandomEvent();
            }
        }
    }
}
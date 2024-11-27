using Extern;
using Manager;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DevTools))]
    public class DevToolsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DevTools storage = (DevTools)target;
            GUIStyle warningStyle = new() { normal = { textColor = Color.red } };


            if (GUILayout.Button("DevTool", EditorStyles.boldLabel))
            {
                storage.showHidden = !storage.showHidden;
                EditorUtility.SetDirty(storage);
            }

            if (storage.showHidden)
            {
                SerializedProperty trapdoor = serializedObject.FindProperty("trapdoorController");
                EditorGUILayout.PropertyField(trapdoor);
                
                serializedObject.ApplyModifiedProperties();
            }

            bool playing = Application.isPlaying;

            // Runtime
            GUI.enabled = playing;
            GUILayout.Label("Runtime", EditorStyles.centeredGreyMiniLabel);
            
            GUILayout.Label("TimeAndEventManager");

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Trigger Random Event")) TimeManager.TriggerRandomEvent();
            if (GUILayout.Button("StartTime"))TimeManager.Instance.StartTimerActive();
            GUILayout.EndHorizontal();
            
            GUILayout.Label("Creatures");
            if (GUILayout.Button("Get Distribution")) CreatureCreator.PrintDistribution();
            
            GUILayout.Label("Music");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("StartMusic"))
            {
                Debug.Log("Starting Music");
                AudioManager.StartStopMusic(false);
            }

            if (GUILayout.Button("StopMusic"))
            {
                Debug.Log("Stoping Music");
                AudioManager.StartStopMusic(true);
            }
            GUILayout.EndHorizontal();
            
            // Always
            
            GUI.enabled = true;
            GUILayout.Label("Runtime/Editor", EditorStyles.centeredGreyMiniLabel);
            GUILayout.Label("Trapdoor");
            if (storage.trapdoorController == null)
            {
                GUILayout.Label("No Trap Door Controller selected", warningStyle);
            }
            else
            {
                if (GUILayout.Button("Open/Close Trapdoor"))
                    storage.trapdoorController.SetOpen(!storage.trapdoorController.IsOpen);
            }
        }
    }
}
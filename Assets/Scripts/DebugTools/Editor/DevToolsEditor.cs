using Data;
using Manager;
using UnityEditor;
using UnityEngine;
using EventHandler = Manager.EventHandler;

namespace DebugTools.Editor
{
    [CustomEditor(typeof(DevTools))]
    public class DevToolsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DevTools storage = (DevTools)target;
            GUIStyle warningStyle = new() { normal = { textColor = Color.red } };

            
            if (GUILayout.Button("DevTool", EditorStyles.boldLabel)) storage.showHidden = !storage.showHidden;

            if (storage.showHidden)
            {
                SerializedProperty window = serializedObject.FindProperty("windowController");
                EditorGUILayout.PropertyField(window);
                
                SerializedProperty creature = serializedObject.FindProperty("creatureController");
                EditorGUILayout.PropertyField(creature);
                
                SerializedProperty doorOne = serializedObject.FindProperty("firstDoorController");
                EditorGUILayout.PropertyField(doorOne);
                
                SerializedProperty doorTwo = serializedObject.FindProperty("secondDoorController");
                EditorGUILayout.PropertyField(doorTwo);
                
                serializedObject.ApplyModifiedProperties();
            }

            bool playing = Application.isPlaying;

            // Runtime
            GUI.enabled = playing;
            GUILayout.Label("Runtime", EditorStyles.centeredGreyMiniLabel);
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Go to Player Camera")) CameraManager.SwitchCamera(true);
            if (GUILayout.Button("Go to Sitting Camera")) CameraManager.SwitchCamera(false);
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Trigger Random Event (Runtime)")) EventHandler.TriggerRandomEvent();

            if (storage.creatureController == null)
            {
                GUILayout.Label("No Creature Controller selected", warningStyle);
            }
            else
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Generate RandomCreature")) storage.creatureController.SetToCreature(CreatureCreator.GetRandomCreature()); 
                if (GUILayout.Button("ResetCreatureController")) storage.creatureController.ResetCreature();
                GUILayout.EndHorizontal();
            }
            
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
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("StartTime"))TimeManager._instance.SetDayTimerActive();
            GUILayout.EndHorizontal();
            
            // Always
            
            GUI.enabled = true;
            GUILayout.Label("Runtime/Editor", EditorStyles.centeredGreyMiniLabel);

            if (storage.windowController == null)
            {
                GUILayout.Label("No Window Controller selected", warningStyle);
            }
            else
            {
                if (GUILayout.Button("Open/Close Window")) storage.windowController.SetWindowOpened(!storage.windowController.IsOpen); 
            }

            if (storage.firstDoorController == null)
            {
                GUILayout.Label("No First Door Controller selected", warningStyle);
            }
            else
            {
                if (GUILayout.Button("Open/Close Door1")) storage.firstDoorController.SetDoorOpend(!storage.firstDoorController.IsOpen);
            }

            if (storage.secondDoorController == null)
            {
                GUILayout.Label("No Second Door Controller selected", warningStyle);
            }
            else
            {
                if (GUILayout.Button("Open/Close Door2")) storage.secondDoorController.SetDoorOpend(!storage.secondDoorController.IsOpen);
            }
        }
    }
}
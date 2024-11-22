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
                SerializedProperty window = serializedObject.FindProperty("windowController");
                EditorGUILayout.PropertyField(window);
                
                SerializedProperty creature = serializedObject.FindProperty("creatureController");
                EditorGUILayout.PropertyField(creature);
                
                SerializedProperty doorOne = serializedObject.FindProperty("firstDoorController");
                EditorGUILayout.PropertyField(doorOne);
                
                SerializedProperty doorTwo = serializedObject.FindProperty("secondDoorController");
                EditorGUILayout.PropertyField(doorTwo);
                
                SerializedProperty trapdoor = serializedObject.FindProperty("trapdoorController");
                EditorGUILayout.PropertyField(trapdoor);
                
                serializedObject.ApplyModifiedProperties();
            }

            bool playing = Application.isPlaying;

            // Runtime
            GUI.enabled = playing;
            GUILayout.Label("Runtime", EditorStyles.centeredGreyMiniLabel);
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Go to Player Mode")) PlaymodeManager.SwitchState(true);
            GUI.enabled = false;
            if (GUILayout.Button("Sitting (Deprecated)")) PlaymodeManager.SwitchState(false);
            GUI.enabled = playing;
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Trigger Random Event")) TimeManager.TriggerRandomEvent();

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
            if (GUILayout.Button("StartTime"))TimeManager.Instance.StartTimerActive();
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get Distribution")) CreatureCreator.PrintDistribution();
            
            // Always
            
            GUI.enabled = true;
            GUILayout.Label("Runtime/Editor", EditorStyles.centeredGreyMiniLabel);

            GUILayout.BeginHorizontal();
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
                if (GUILayout.Button("Open/Close Door1")) storage.firstDoorController.SetDoorOpened(!storage.firstDoorController.IsOpen);
            }

            if (storage.secondDoorController == null)
            {
                GUILayout.Label("No Second Door Controller selected", warningStyle);
            }
            else
            {
                if (GUILayout.Button("Open/Close Door2")) storage.secondDoorController.SetDoorOpened(!storage.secondDoorController.IsOpen);
            }
            GUILayout.EndHorizontal();

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
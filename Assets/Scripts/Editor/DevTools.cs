using Manager;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DevTools : EditorWindow
    {
        [MenuItem("Window/DevTools")]
        public static void ShowWindow() => GetWindow(typeof(DevTools));
        
        private void OnGUI()
        {
            GUI.enabled = Application.isPlaying;
            
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
        }
    }
}
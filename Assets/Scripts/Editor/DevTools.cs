using Controller.UI.Panels;
using Data;
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
            
            GUILayout.Label("Events");
            if (GUILayout.Button("Trigger Random Event")) TimeManager.TriggerRandomEvent();
            
            GUILayout.Label("Creatures");
            if (GUILayout.Button("Get Distribution")) CreatureCreator.PrintDistribution();
            
            GUILayout.Label("Audio");
            if (GUILayout.Button("Test Audio")) AudioManager.PlayEffect(AudioEffect.Knocking, new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-1.5f, 1.5f)));

            GUILayout.Label("Creature Control");
            if (GUILayout.Button("Spawn Evil"))GameManager.Creature.SetToCreature(CreatureCreator.GetEvil());
            
            GUILayout.Label("Trigger Game Over");
            if(GUILayout.Button("GameOver")) GameOverUIController.Instance.GameOver();
        }
    }
}
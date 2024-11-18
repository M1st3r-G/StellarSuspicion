using Controller;
using Manager;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CreatureController))]
    public class CreatureControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label("Dev Tools", EditorStyles.boldLabel);

            if (GUILayout.Button("Set to Random Creature (Runtime)"))
            {
                CreatureController creatureController = (CreatureController)target;
                creatureController.SetToCreature(CreatureCreator.GetRandomCreature());
            }
        }
    }
}
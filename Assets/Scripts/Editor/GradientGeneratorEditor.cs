using Extern;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GradientGenerator))]
    public class GradientGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GradientGenerator gradientGenerator = (GradientGenerator)target;
            
            if (!GUILayout.Button("Generate PNG Gradient Texture")) return;
            gradientGenerator.BakeGradientTexture();
            AssetDatabase.Refresh();
        }
    }
}
using System.IO;
using UnityEngine;

namespace Extern
{
    public class GradientGenerator : MonoBehaviour
    {
        public Gradient gradient;
        public string savingString = "/Scripts/Extern/Gradients/Generated/";

        public float width = 256;
        public float height = 64;
        
        private Texture2D _gradientTexture;
        private Texture2D _tempTexture;

        private Texture2D GenerateGradientTexture(Gradient grad)
        {
            if (_tempTexture == null)
            {
                _tempTexture = new Texture2D((int)width, (int)height);
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = gradient.Evaluate(x / width);
                    _tempTexture.SetPixel(x, y, color);
                }
            }
            
            _tempTexture.wrapMode = TextureWrapMode.Clamp;
            _tempTexture.Apply();
            return _tempTexture;
        }

        public void BakeGradientTexture()
        {
            _gradientTexture = GenerateGradientTexture(gradient);
            byte[] bytes = _gradientTexture.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath+savingString+"GradientTexture_"+Random.Range(0,999999)+".png", bytes);
        }
    }
}

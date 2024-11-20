using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ConvertAssets
    {
        private static string GetDefaultFile(string name, string guid, int type, int goodness)
            => $"%YAML 1.1\n%TAG !u! tag:unity3d.com,2011:\n--- !u!114 &11400000\nMonoBehaviour:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 0}}\n  m_Enabled: 1\n  m_EditorHideFlags: 0\n  m_Script: {{fileID: 11500000, guid: 5f4ea6fb59444268ae484ce4d6a77111, type: 3}}\n  m_Name: {name}\n  m_EditorClassIdentifier: \n  part:\n    sprite: {{fileID: 21300000, guid: {guid}, type: 3}}\n    type: {type}\n    goodness: {goodness}\n";

        private static string GetDefaultMetaFile(string guid)
            => $"fileFormatVersion: 2\nguid: {guid}\nNativeFormatImporter:\n  externalObjects: {{}}\n  mainObjectFileID: 11400000\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n";
        
        public static void ConvertAsset(string srcPath, string outPath, Dictionary<string, int> typeToInt)
        {
            foreach (string typeFolder in Directory.GetDirectories(srcPath))
            {
                string typeName = Path.GetFileName(typeFolder);
                int type = typeToInt[typeName];
                
                foreach (string numericFolder in Directory.GetDirectories(typeFolder))
                {
                    string numericName = Path.GetFileName(numericFolder);
                    int goodness = int.Parse(numericName);

                    string writePath = $"{outPath}/{(CreatureComponentType)type}/{goodness}/";
                    Directory.CreateDirectory(writePath);
                    
                    foreach (string file in Directory.EnumerateFiles(numericFolder))
                    {
                        if (!Path.GetExtension(file).EndsWith(".meta")) continue;

                        string name = Path.GetFileName(file).Replace(".png.meta", "");

                        string guid = File.ReadLines(file).ElementAt(1).Replace("guid: ", "");
                        string newGuid = GUID.Generate().ToString();               
                        
                        File.WriteAllText($"{writePath}{name}.asset", GetDefaultFile(name, guid, type, goodness));
                        File.WriteAllText($"{writePath}{name}.asset.meta", GetDefaultMetaFile(newGuid));
                    }
                    Debug.LogWarning($"Finished {numericFolder}");
                }
                Debug.LogWarning($"Finished {typeFolder}");
            }
            Debug.LogWarning("Finished");
            AssetDatabase.Refresh();
        }
    }
}

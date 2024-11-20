using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PartImporterToolWindow : EditorWindow
    {
        private string _srcPath = "";
        private string _outPath = "";
        private int _type;
        private readonly Dictionary<string, int> _typeToInt = new();
        private GUIStyle _warningStyle;
        private GUIStyle _errorStyle;
        private bool _showDict;
        private int _warningCount;
        
        private void OnEnable()
        {
            minSize = new Vector2(400, 600);
            _warningStyle = new GUIStyle
            {
                fontStyle = FontStyle.Italic,
                normal = { textColor = Color.yellow }
            };

            _errorStyle = new GUIStyle
            {
                fontStyle = FontStyle.Italic,
                normal = { textColor = Color.red }
            };
        }
        
        [MenuItem("Window/Custom Part Import Tool")]
        public static void ShowWindow() => GetWindow(typeof(PartImporterToolWindow));

        private void OnGUI()
        {
            _warningCount = 0;
            GUILayout.Label ("Folder Parameters", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Select", GUILayout.Width(50)))
            {
                _srcPath = EditorUtility.OpenFolderPanel("Select Image folder", "Assets/", "Monster");
                while(!IsValidSrcDirectory(_srcPath)) 
                    _srcPath = EditorUtility.OpenFolderPanel("Select Image folder", "Assets/", "Monster");
            }
            GUILayout.Label("SrcPath: ", GUILayout.ExpandWidth(false));
            GUILayout.TextField(_srcPath);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select", GUILayout.Width(50))) 
                _outPath = EditorUtility.OpenFolderPanel("Select Image folder", "Assets/", "Monster");
            GUILayout.Label("OutPath: ", GUILayout.ExpandWidth(false));
            GUILayout.TextField(_outPath);
            GUILayout.EndHorizontal();
            
            EditorAssert(_srcPath != "", "Src Path is not set");
            EditorAssert(_outPath != "", "Out Path is not set");

            _showDict = EditorGUILayout.BeginFoldoutHeaderGroup(_showDict, "Path to Type Conversion");

            if (_srcPath == "") return;
            
            string[] folderPaths = Directory.GetDirectories(_srcPath);
            if (_showDict)
            {
                foreach (string folderPath in folderPaths)
                {
                    GUILayout.BeginHorizontal();
                    string typeFolderName = folderPath.Split(Path.DirectorySeparatorChar).Last();
                    GUILayout.Label(typeFolderName, GUILayout.Width(150));

                    int prevValue = _typeToInt.GetValueOrDefault(typeFolderName, 0);
                    _typeToInt[typeFolderName] = (int)(CreatureComponentType)EditorGUILayout.EnumPopup((CreatureComponentType)prevValue);
                    GUILayout.EndHorizontal();
                }
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorAssert(_typeToInt.Count == folderPaths.Length, "There is an Error in the Dictionary");
            EditorAssert(!ContainsDuplicates(_typeToInt), "There are still Duplicates in the Dictionary", true);

            if (_outPath == "") return;
            EditorAssert(IsDirectoryEmpty(_outPath), "The Output does not seem to be empty");

            EditorGUILayout.Separator();
            
            if (_warningCount != 0) return;
            if (!GUILayout.Button("Start Conversion")) return;
            
            ConvertAssets.ConvertAsset(_srcPath, _outPath, _typeToInt);
            return;
            EditorUtility.DisplayDialog("AssetConversion Finished Successfully",
                "This window will now be closed", "Understood");
            Close();
        }

        private void EditorAssert(bool test, string display, bool warning = false)
        {
            if (test) return;

            _warningCount++;
            GUILayout.Label(display, warning ? _warningStyle : _errorStyle);
        }

        private bool IsValidSrcDirectory(string path)
        {
            return _srcPath != "" && Directory.EnumerateDirectories(path).All(typeFolder =>
                Directory.EnumerateDirectories(typeFolder).All(CheckNumericFolder));
        }

        private bool CheckNumericFolder(string path)
        {
            if (!int.TryParse(Path.GetFileNameWithoutExtension(path), out _)) return false;

            return !ContainsFolder(path) && Directory.EnumerateFiles(path).All(FittingFileEnd);

            bool ContainsFolder(string s)
            {
                using IEnumerator<string> dirs = Directory.EnumerateDirectories(s).GetEnumerator();
                return dirs.MoveNext();
            }

            bool FittingFileEnd(string endfiles)
            {
                string fileEnd = Path.GetExtension(endfiles);
                return fileEnd.EndsWith(".png") || fileEnd.EndsWith(".meta");
            }
        }

        private static bool IsDirectoryEmpty(string path) => !Directory.EnumerateFileSystemEntries(path).Any();
        private static bool ContainsDuplicates(Dictionary<string, int> dict) => dict.Count != dict.DistinctBy(pair => pair.Value).Count();
    }
}

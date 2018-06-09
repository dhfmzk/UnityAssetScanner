using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace OrcaAssistTools {

    // Editor Type
    public enum ToolType {
        Finder = 0,
        Researcher = 1,
    };

	public partial class AssetScannerWindow : EditorWindow {

        private Object searchedObject;
        private Dictionary<Object, int> referenceObjects = new Dictionary<Object, int>();
        private Vector2 scrollPosition;
        private Stopwatch searchTimer = new Stopwatch();

        private AssetScanner worker = new AssetScanner();

        [MenuItem("OrcaAssist/Reference Finder")]
        static void Init() {
            GetWindow(typeof(AssetScannerWindow), false, "Asset Scanner");
        }

        private int GUI_SelectedTerm = 0;
        private bool GUI_ShowCallback = false;
        void OnGUI() {
            GUI_SelectedTerm = DrawTabs(GUI_SelectedTerm, System.Enum.GetNames(typeof(ToolType)));
            switch (GUI_SelectedTerm) {
                case (int)ToolType.Finder:
                    DrawEditorFinder();
                    break;
                case (int)ToolType.Researcher:
                    DrawEditorResearcher();
                    break;
                default:
                    break;
            }
        }

        public int DrawTabs(int Index, string[] Tabs, GUIStyle Style = null, int height = 25, bool expand = true) {
            GUIStyle MyStyle = new GUIStyle(Style != null ? Style : GUI.skin.FindStyle("dragtab"));
            MyStyle.fixedHeight = 0;

            GUILayout.BeginHorizontal();
            for (int i = 0; i < Tabs.Length; ++i) {
                int idx = Tabs[i].IndexOf('|');
                if (idx > 0) {
                    string text = Tabs[i].Substring(0, idx);
                    string tooltip = Tabs[i].Substring(idx + 1);
                    if (GUILayout.Toggle(Index == i, new GUIContent(text, tooltip), MyStyle, GUILayout.Height(height), GUILayout.ExpandWidth(expand)) && Index != i) {
                        Index = i;
                        GUI.FocusControl(string.Empty);
                    }
                }
                else {
                    if (GUILayout.Toggle(Index == i, Tabs[i], MyStyle, GUILayout.Height(height), GUILayout.ExpandWidth(expand)) && Index != i) {
                        Index = i;
                        GUI.FocusControl(string.Empty);
                    }
                }
            }
            // TODO : Draw Description button using icon
            GUILayout.EndHorizontal();
            return Index;
        }

        private string[] DisplayLayoutGetTargetAsset() {

            EditorGUILayout.BeginHorizontal();
            searchedObject = EditorGUILayout.ObjectField(searchedObject != null ? searchedObject.name : "Drag & Drop Asset", searchedObject, typeof(Object), false);
            EditorGUILayout.EndHorizontal();

            // Find path for get GUID and return it!
            return new string[] { AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(searchedObject)) };
        }
	}

}
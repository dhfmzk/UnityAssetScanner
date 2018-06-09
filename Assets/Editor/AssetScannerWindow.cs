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

        private Object _searchedObject;
        private Dictionary<Object, int> _referenceObjects = new Dictionary<Object, int>();
        private Vector2 _scrollPosition;
        private Stopwatch _searchTimer = new Stopwatch();

        private readonly AssetScanner _worker = new AssetScanner();

        [MenuItem("OrcaAssist/Reference Finder")]
        private static void Init() {
            GetWindow(typeof(AssetScannerWindow), false, "Asset Scanner");
        }

        private int _guiSelectedTerm = 0;
        private bool _guiShowCallback = false;

	    private void OnGUI() {
            _guiSelectedTerm = DrawTabs(_guiSelectedTerm, System.Enum.GetNames(typeof(ToolType)));
            switch (_guiSelectedTerm) {
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

        public int DrawTabs(int index, string[] tabs, GUIStyle style = null, int height = 25, bool expand = true) {
            GUIStyle myStyle = new GUIStyle(other: style ?? GUI.skin.FindStyle("dragtab")) {fixedHeight = 0};

            GUILayout.BeginHorizontal();
            for (int i = 0; i < tabs.Length; ++i) {
                int idx = tabs[i].IndexOf('|');
                if (idx > 0) {
                    string text = tabs[i].Substring(0, idx);
                    string tooltip = tabs[i].Substring(idx + 1);
                    if (!GUILayout.Toggle(index == i, new GUIContent(text, tooltip), myStyle, GUILayout.Height(height),
                            GUILayout.ExpandWidth(expand)) || index == i) continue;
                    index = i;
                    GUI.FocusControl(string.Empty);
                }
                else {
                    if (!GUILayout.Toggle(index == i, tabs[i], myStyle, GUILayout.Height(height),
                            GUILayout.ExpandWidth(expand)) || index == i) continue;
                    index = i;
                    GUI.FocusControl(string.Empty);
                }
            }

            GUILayout.EndHorizontal();
            return index;
        }

        private string[] DisplayLayoutGetTargetAsset() {

            EditorGUILayout.BeginHorizontal();
            _searchedObject = EditorGUILayout.ObjectField(_searchedObject != null ? _searchedObject.name : "Drag & Drop Asset", _searchedObject, typeof(Object), false);
            EditorGUILayout.EndHorizontal();

            // Find path for get GUID and return it!
            return new string[] { AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_searchedObject)) };
        }
	}

}
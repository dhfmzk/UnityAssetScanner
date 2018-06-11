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
	    private Vector2 _scrollPosition;
	    private static float _tabBarHeight = 28.0f;

        private Dictionary<Object, int> _referenceObjects = new Dictionary<Object, int>();
        private Stopwatch _searchTimer = new Stopwatch();

        private readonly AssetScanner _worker = new AssetScanner();

        [MenuItem("OrcaAssist/Asset Scanner")]
        private static void Init() {
            GetWindow(typeof(AssetScannerWindow), false, "Asset Scanner");
        }

	    private void OnInspectorUpdate() {
	        Repaint();
	    }

        private int _activeTabIndex = 0;
	    private void OnGUI() {

	        DrawTabs(ref _activeTabIndex, System.Enum.GetNames(typeof(ToolType)));

	        DrawHeader();
            DrawBody(_activeTabIndex);
	        DrawFooter();
	    }

	    public void DrawTabs(ref int index, string[] tabs, GUIStyle style = null, int height = 25, bool expand = true) {
	        GUIStyle myStyle = new GUIStyle(other: style ?? GUI.skin.FindStyle("dragtab")) { fixedHeight = 0 };

	        GUILayout.BeginHorizontal();
	        for(int i = 0; i < tabs.Length; ++i) {
	            int idx = tabs[i].IndexOf('|');
	            if(idx > 0) {
	                string text = tabs[i].Substring(0, idx);
	                string tooltip = tabs[i].Substring(idx + 1);
	                if(!GUILayout.Toggle(index == i, new GUIContent(text, tooltip), myStyle, GUILayout.Height(height),
	                       GUILayout.ExpandWidth(expand)) || index == i) continue;
	                index = i;
	                GUI.FocusControl(string.Empty);
	            }
	            else {
	                if(!GUILayout.Toggle(index == i, tabs[i], myStyle, GUILayout.Height(height),
	                       GUILayout.ExpandWidth(expand)) || index == i) continue;
	                index = i;
	                GUI.FocusControl(string.Empty);
	            }
	        }

	        GUILayout.EndHorizontal();
	    }

	    private void DrawHeader() {

	    }

	    private void DrawBody(int item) {
	        switch(item) {
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

	    private void DrawFooter() {

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
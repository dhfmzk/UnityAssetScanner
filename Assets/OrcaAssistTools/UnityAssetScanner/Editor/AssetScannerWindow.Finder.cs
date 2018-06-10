using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace OrcaAssistTools {

    public partial class AssetScannerWindow {
        private void DrawEditorFinder() {
            // Get target object GUID

            // Button for start researching
            if (GUILayout.Button("Find it!")) {
            }

            // Draw result
            DrawFindResultWindow();
        }

        private void DrawFindResultWindow() {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach(ScanResultInfo element in _researchedResult) {
                element.DrawEditor();
            }

            GUILayout.EndScrollView();
        }
	}

}
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

        private List<ScanResultInfo> _researchedResult = new List<ScanResultInfo>();
        private void DrawEditorResearcher() {
            // Get target object guid
            string[] targetGuids = DisplayLayoutGetTargetAsset();

            // Button for start researching
            if (GUILayout.Button("Research it!")) {
                _worker.ResearchReference(targetGuids, ref _researchedResult);
            }

            foreach(ScanResultInfo info in _researchedResult) {
                Debug.Log(info.RootObject);
            }
            // Draw result
            DrawResearchResultWindow();
        }

        private void DrawResearchResultWindow() {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach (ScanResultInfo element in _researchedResult) {
                element.DrawEditorUi();
            }

            GUILayout.EndScrollView();
        }
    }
}

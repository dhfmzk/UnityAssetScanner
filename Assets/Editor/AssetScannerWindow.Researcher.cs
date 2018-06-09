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

        private List<ScanResultInfo> ResearchedResult = new List<ScanResultInfo>();
        private void DrawEditorResearcher() {
            // Get target object guid
            string[] targetGuids = DisplayLayoutGetTargetAsset();

            // Button for start researching
            if (GUILayout.Button("Research it!")) {
                worker.ResearchReference(targetGuids, ref ResearchedResult);
            }

            foreach(ScanResultInfo info in ResearchedResult) {
                Debug.Log(info.rootObject);
            }
            // Draw result
            DrawResearchResultWindow();
        }

        private void DrawResearchResultWindow() {

        }
    }
}

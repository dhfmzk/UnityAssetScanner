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
                // worker.FindReference(targetGuid, ref ResearchedDict);
            }

            // Draw result
            DrawFindResultWindow();
        }

        private void DrawFindResultWindow() {

        }
	}

}
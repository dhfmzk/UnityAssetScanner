using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using Object = UnityEngine.Object;
using UnityEngine;

namespace OrcaAssistTools {

	public class AssetScanner {
	    private List<KeyValuePair<Object, int>> _searchedAssetList;

        public void ResearchReference(string[] guids, ref List<ScanResultInfo> result) {
            result = guids.Select(ResearchAllReference).ToList();
        }

		public void ResearchReference(string folderPath, ref List<ScanResultInfo> result) {

		}
        
		private ScanResultInfo ResearchAllReference(string guid) {
			// Get target path
			string targetPath = AssetDatabase.GUIDToAssetPath(guid);
			Object targetObject = AssetDatabase.LoadAssetAtPath<Object>(targetPath);

		    if (targetObject == null) {
                return new ScanResultInfo();
            }

            string[] relatedAssetsPaths = AssetDatabase.GetDependencies(targetPath);

            _searchedAssetList =  relatedAssetsPaths.Distinct()
                                                    .Select(selector: path => new KeyValuePair<Object, int>(AssetDatabase.LoadAssetAtPath<Object>(path),
                                                            value: relatedAssetsPaths.Count(x => string.Equals(x, path))))
                                                    .ToList();
            return new ScanResultInfo(targetObject, _searchedAssetList);
        }

        private void FindReference(string guid, ref Dictionary<Object, int> dict) {
            
        }
	}
}
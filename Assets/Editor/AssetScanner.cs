using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OrcaAssistTools {

	public class AssetScanner {

/// -----------------------------------------------------------------------------
///		Public worker
/// -----------------------------------------------------------------------------
        
		public void ResearchReference(string[] _guids, ref List<ScanResultInfo> _result) {
			List<ScanResultInfo> temp = _guids.Select(guid => ResearchAllReference(guid)).ToList();
			// TODO : Filltering
		}

		public void ResearchReference(string _folderPath, ref List<ScanResultInfo> _result) {

		}

/// -----------------------------------------------------------------------------
///		Private worker
/// -----------------------------------------------------------------------------
        
		private ScanResultInfo ResearchAllReference(string _guid) {

			ScanResultInfo ret = new ScanResultInfo();
			// Get target path
			string targetPath = AssetDatabase.GUIDToAssetPath(_guid);
			Object targetObject = AssetDatabase.LoadAssetAtPath<Object>(targetPath);

			if (targetObject == null) {
				return ret;
			}

			ret.rootObject = targetObject;

			string[] relatedAssetsPaths = AssetDatabase.GetDependencies(targetPath);
            
			List<KeyValuePair<Object, int>> searchedAssetList;
            searchedAssetList =   relatedAssetsPaths.Distinct()
													.Select(path => new KeyValuePair<Object, int>(AssetDatabase.LoadAssetAtPath<Object>(path), relatedAssetsPaths.Count(x => string.Equals(x, path))))
													.ToList();

            foreach (KeyValuePair<Object, int> searchedAsset in searchedAssetList) {
                Debug.Log(searchedAsset.Key);
            }

			return ret;
        }

        private void FindReference(string _guid, ref Dictionary<Object, int> dict) {
            
        }
	}


    public class ScanResultInfo {
		// Root Object
		public Object rootObject;

		// Object : Dependency Object/Asset
		// Object : location (under root, path string)
		public List<Object> resultList;
	}

}
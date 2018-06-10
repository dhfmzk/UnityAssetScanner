using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;
using UnityEngine;

namespace OrcaAssistTools {

	public class AssetScanner {
	    private List<KeyValuePair<Object, int>> _searchedAssetList;

        public void ResearchReference(string[] guids, ref List<ScanResultInfo> result) {
			List<ScanResultInfo> temp = guids.Select(ResearchAllReference).ToList();
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
            
            return new ScanResultInfo(targetObject, _searchedAssetList); ;
        }

        private void FindReference(string guid, ref Dictionary<Object, int> dict) {
            
        }
	}


    public class ScanResultInfo {
        public Object RootObject { get; set; }
        public List<KeyValuePair<Object,int>> ResultList { get; set; }

        public ScanResultInfo(Object rootObject, List<KeyValuePair<Object, int>> resultList) {
            RootObject = rootObject;
            ResultList = resultList;
        }

        public ScanResultInfo() {
        }

        public override bool Equals(object obj) {
            ScanResultInfo info = obj as ScanResultInfo;
            return info != null &&
                   EqualityComparer<Object>.Default.Equals(RootObject, info.RootObject) &&
                   object.Equals(ResultList, info.ResultList);
        }

        public override string ToString() {
            return base.ToString();
        }

        public override int GetHashCode() {
            int hashCode = -1138907818;
            hashCode = hashCode * -1521134295 + EqualityComparer<Object>.Default.GetHashCode(RootObject);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<KeyValuePair<Object, int>>>.Default.GetHashCode(ResultList);
            return hashCode;
        }

        public void DrawEditorUi() {
            // Draw Self
        }
    }
    
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OrcaAssistTools {
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

        private bool _isOpen = false;
        public void DrawEditor() {
            _isOpen = EditorGUILayout.Foldout(_isOpen, RootObject.name);
            if (!_isOpen) return;
            
            foreach (KeyValuePair<Object, int> objectinfoKeyValuePair in ResultList) {
                EditorGUILayout.LabelField(objectinfoKeyValuePair.Value + " - " + objectinfoKeyValuePair.Key.name);
            }
        }
    }
}
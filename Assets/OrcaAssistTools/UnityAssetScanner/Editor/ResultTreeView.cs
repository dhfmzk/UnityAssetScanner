using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class ResultTreeView : TreeView {
    
    private const float RowHeight = 20f;
    private const int StartElementId = 1;

    private enum ColumnType {
        Index,
        Root,
        Name,
        Location,
        Controll
    }

    private GUIStyle _textAreaStyle;
    private float _valueColumnWidth = 0;
    private int _elementId = StartElementId;

    public ResultTreeView(TreeViewState state) : base(state) {

    }

    public ResultTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader) {

        _textAreaStyle = new GUIStyle(EditorStyles.textArea) { wordWrap = true };

        base.rowHeight = rowHeight;
        columnIndexForTreeFoldouts = 1;
        showAlternatingRowBackgrounds = true;
        showBorder = true;
        
        customFoldoutYOffset = (rowHeight - EditorGUIUtility.singleLineHeight) * 0.5f;
        multiColumnHeader.canSort = false;
        Reload();
    }

    protected override TreeViewItem BuildRoot() {
        throw new System.NotImplementedException();
    }
}

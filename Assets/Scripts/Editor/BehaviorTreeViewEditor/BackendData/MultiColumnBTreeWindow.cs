using Assets.Scripts.AI;
using Assets.Scripts.AI.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreeViewEditor.BackendData
{
    class MultiColumnBTreeWindow : EditorWindow
    {
        [NonSerialized] bool _Initialized;
        [SerializeField] TreeViewState _TreeViewState; // Serialized in the window layout file so it survives assembly reloading
        [SerializeField] MultiColumnHeaderState _MultiColumnHeaderState;
        SearchField _SearchField;
        MultiColumnBehaviorTreeView _TreeView;
        BehaviorTreeAsset _BehaviorTreeAsset;

        [MenuItem("Behavior Tree/New Tree")]
        public static MultiColumnBTreeWindow GetWindow()
        {
            var window = GetWindow<MultiColumnBTreeWindow>();
            window.titleContent = new GUIContent("Behavior Tree Builder");
            window.Focus();
            window.Repaint();
            return window;
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var BTreeAsset = EditorUtility.InstanceIDToObject(instanceID) as BehaviorTreeAsset;
            if (BTreeAsset != null)
            {
                var window = GetWindow();
                window.SetTreeAsset(BTreeAsset);
                return true;
            }
            return false; // we did not handle the open
        }

        public void SetTreeAsset(BehaviorTreeAsset BehaviorTreeAsset)
        {
            if(BehaviorTreeAsset == null)
            {
                CreateNewTree();
            }
            _BehaviorTreeAsset = BehaviorTreeAsset;
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(_BehaviorTreeAsset);
            _Initialized = false;
        }

        Rect multiColumnTreeViewRect
        {
            get { return new Rect(20, 50, position.width - 40, position.height - 70); }
        }

        Rect toolbarRect
        {
            get { return new Rect(20f, 10f, position.width - 40f, 20f); }
        }

        Rect topToolbarRect
        {
            get { return new Rect(20f, 30f, position.width - 40f, 30f); }
        }

        Rect bottomToolbarRect
        {
            get { return new Rect(20f, position.height - 18f, position.width - 60f, 16f); }
        }

        public MultiColumnBehaviorTreeView treeView
        {
            get { return _TreeView; }
        }

        void InitIfNeeded()
        {
            if (!_Initialized)
            {
                // Check if it already exists (deserialized from window layout file or scriptable object)
                if (_TreeViewState == null)
                    _TreeViewState = new TreeViewState();

                bool firstInit = _MultiColumnHeaderState == null;
                var headerState = MultiColumnBehaviorTreeView.CreateDefaultMultiColumnHeaderState(multiColumnTreeViewRect.width);
                if (MultiColumnHeaderState.CanOverwriteSerializedFields(_MultiColumnHeaderState, headerState))
                    MultiColumnHeaderState.OverwriteSerializedFields(_MultiColumnHeaderState, headerState);
                _MultiColumnHeaderState = headerState;

                var multiColumnHeader = new BTreeMultiColumnHeader(headerState);
                if (firstInit)
                    multiColumnHeader.ResizeToFit();

                var treeModel = new TreeModel<BehaviorTreeElement>(GetData());

                _TreeView = new MultiColumnBehaviorTreeView(_TreeViewState, multiColumnHeader, treeModel);

                _SearchField = new SearchField();
                _SearchField.downOrUpArrowKeyPressed += _TreeView.SetFocusAndEnsureSelectedItem;

                _Initialized = true;
            }
        }

        IList<BehaviorTreeElement> GetData()
        {
            if (_BehaviorTreeAsset == null)
            {
                CreateNewTree();
            }

            if(_BehaviorTreeAsset.treeElements == null || _BehaviorTreeAsset.treeElements.Count <= 0)
            {
                _BehaviorTreeAsset.treeElements.Add(new BehaviorTreeElement("root", -1, 0));
            }

            AssetDatabase.Refresh();
            EditorUtility.SetDirty(_BehaviorTreeAsset);
            return _BehaviorTreeAsset.treeElements;
        }

        void CreateNewTree()
        {
            _BehaviorTreeAsset = CreateInstance<BehaviorTreeAsset>();
            AssetDatabase.CreateAsset(_BehaviorTreeAsset, "Assets/Scripts/AI/BehaviorTrees/" + name + ".asset");
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(_BehaviorTreeAsset);
        }

        void OnSelectionChange()
        {
            if (!_Initialized)
                return;

            var BehaviorTreeAsset = Selection.activeObject as BehaviorTreeAsset;
            if (BehaviorTreeAsset != null && BehaviorTreeAsset != _BehaviorTreeAsset)
            {
                _BehaviorTreeAsset = BehaviorTreeAsset;
                _TreeView.treeModel.SetData(GetData());
                _TreeView.Reload();
            }
        }

        void OnGUI()
        {
            InitIfNeeded();

            SearchBar(toolbarRect);
            TopToolbar(topToolbarRect);
            DoTreeView(multiColumnTreeViewRect);
            BottomToolBar(bottomToolbarRect);
        }

        void SearchBar(Rect rect)
        {
            treeView.searchString = _SearchField.OnGUI(rect, treeView.searchString);
        }

        void DoTreeView(Rect rect)
        {
            _TreeView.OnGUI(rect);
        }

        void TopToolbar(Rect rect)
        {
            GUILayout.BeginArea(rect);

            using (new EditorGUILayout.HorizontalScope())
            {
                GenericMenu menu = new GenericMenu();

                var style = "miniButton";
                if (EditorGUILayout.DropdownButton(new GUIContent("Add Behavior"), FocusType.Passive, style, GUILayout.Width(130)))
                {
                    foreach (var elType in BehaviorTreeViewExtensions.GetListOfTypes<BehaviorTreeElement>())
                    {
                        menu.AddItem(new GUIContent(elType.ToString()), false, OnTypeSelected, elType.ToString());
                    }
                    menu.ShowAsContext();
                }
                if(GUILayout.Button("Save Tree"))
                {
                    TreeElementUtility.TreeToList(_TreeView.treeModel.root, _BehaviorTreeAsset.treeElements);
                    Debug.Log("Count:" + _BehaviorTreeAsset.treeElements.Count);
                    SaveAsset();
                }
            }

            GUILayout.EndArea();
        }

        private void OnTypeSelected(object typeName)
        {
            var selection = _TreeView.GetSelection();
            BehaviorTreeElement parent = (selection.Count == 1 ? _TreeView.treeModel.Find(selection[0]) : null) ?? _TreeView.treeModel.root;
            int depth = parent != null ? parent.depth + 1 : 0;
            int id = _TreeView.treeModel.GenerateUniqueID();

            Debug.Log(typeName);

            Type type = typeof(BehaviorTreeElement).Assembly.GetType((string)typeName, true);
            var element = Activator.CreateInstance(type, type.ToString() + " " + id ,depth, id);
            _TreeView.treeModel.AddElement((BehaviorTreeElement)element, parent, 0);
            _TreeView.SetSelection(new[] { id }, TreeViewSelectionOptions.RevealAndFrame);
        }

        void SaveAsset()
        {
            EditorUtility.SetDirty(_BehaviorTreeAsset);
            AssetDatabase.SaveAssets();
        }

        void BottomToolBar(Rect rect)
        {
            GUILayout.BeginArea(rect);

            using (new EditorGUILayout.HorizontalScope())
            {
                var style = "miniButton";
                if (GUILayout.Button("Expand All", style))
                {
                    treeView.ExpandAll();
                }

                if (GUILayout.Button("Collapse All", style))
                {
                    treeView.CollapseAll();
                }

                GUILayout.FlexibleSpace();

                GUILayout.Label(_BehaviorTreeAsset != null ? AssetDatabase.GetAssetPath(_BehaviorTreeAsset) : "No Asset Loaded");
                GUILayout.Space(10);
            }

            GUILayout.EndArea();
        }
    }

    internal class BTreeMultiColumnHeader : MultiColumnHeader
    {
        Mode m_Mode;

        public enum Mode
        {
            LargeHeader,
            DefaultHeader,
            MinimumHeaderWithoutSorting
        }

        public BTreeMultiColumnHeader(MultiColumnHeaderState state)
            : base(state)
        {
            mode = Mode.DefaultHeader;
        }

        public Mode mode
        {
            get
            {
                return m_Mode;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case Mode.LargeHeader:
                        canSort = true;
                        height = 37f;
                        break;
                    case Mode.DefaultHeader:
                        canSort = true;
                        height = DefaultGUI.defaultHeight;
                        break;
                    case Mode.MinimumHeaderWithoutSorting:
                        canSort = false;
                        height = DefaultGUI.minimumHeight;
                        break;
                }
            }
        }

        protected override void ColumnHeaderGUI(MultiColumnHeaderState.Column column, Rect headerRect, int columnIndex)
        {
            // Default column header gui
            base.ColumnHeaderGUI(column, headerRect, columnIndex);

            // Add additional info for large header
            if (mode == Mode.LargeHeader)
            {
                // Show example overlay stuff on some of the columns
                if (columnIndex > 2)
                {
                    headerRect.xMax -= 3f;
                    var oldAlignment = EditorStyles.largeLabel.alignment;
                    EditorStyles.largeLabel.alignment = TextAnchor.UpperRight;
                    GUI.Label(headerRect, 36 + columnIndex + "%", EditorStyles.largeLabel);
                    EditorStyles.largeLabel.alignment = oldAlignment;
                }
            }
        }


    }

}

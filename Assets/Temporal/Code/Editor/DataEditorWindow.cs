using System;
using System.Collections.Generic;
using System.Linq;
using Code.DataConfig.BaseObjects;
using Code.DataConfig.DataLists;
using Code.DataConfig.Tools;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
namespace Code.Editor
{
    public class DataEditorWindow : OdinMenuEditorWindow
    {
        private DataPath _dataPath;
        private static DataList _dataList;
        private Vector2 _scroll;

        [ListItemSelector("SetSelected")]
        public List<BaseBlock> blockList = new List<BaseBlock>();

        [BoxGroup("Preview")]
        [ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        private BaseBlock _selectedBlock;

        [MenuItem("BlockBreaker/OdinTest")]
        private static void OpenWindow()
        {
            GetWindow<DataEditorWindow>().Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _dataPath = CreateInstance<DataPath>();
            _dataList = AssetDatabase.LoadAssetAtPath<DataList>(_dataPath.dataListPath);
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(supportsMultiSelect: false){
                {
                    "Settings", _dataList.dataPath, EditorIcons.SettingsCog
                },{
                    "Show Data", ""
                },{
                    "Show Data/Show Level", _dataList.baseLevelList
                },{
                    "Show Data/Show Block", _dataList.baseBlockList
                },{
                    "Show Data/Show Booster", ""
                },{
                    "Create Data", ""
                },{
                    "Create Data/Create Level", new CreateLevelData(_dataList)
                },{
                    "Create Data/Create Block", new CreateBlockData(_dataList)
                },{
                    "Create Data/Create Booster", ""
                },
            };

            tree.Config.DrawScrollView = true;
            tree.Config.DrawSearchToolbar = true;
            // tree.SortMenuItemsByName();

            tree.DefaultMenuStyle = new OdinMenuStyle{
                Height = 30, Offset = 16.00f, IndentAmount = 15.00f, IconSize = 28.00f,
                IconOffset = 0.00f, NotSelectedIconAlpha = 0.85f, IconPadding = 3.00f, TriangleSize = 17.00f,
                TrianglePadding = 8.00f, AlignTriangleLeft = false, Borders = true, BorderPadding = 13.00f,
                BorderAlpha = 0.50f, SelectedColorDarkSkin = new Color(0.243f, 0.373f, 0.588f, 1.000f), SelectedColorLightSkin = new Color(0.243f, 0.490f, 0.900f, 1.000f)
            };


            tree.AddAllAssetsAtPath("Show Data/Show Level", _dataPath.levelsPath).SortMenuItemsByName();

            tree.AddAllAssetsAtPath("Show Data/Show Block", _dataPath.blocksPath).SortMenuItemsByName().ForEach(this.AddDragHandles);
            ;

            tree.EnumerateTree().AddIcons<BaseBlock>(x => x.blockIcon);

            tree.DrawMenuTree();

            tree.MarkDirty();

            // this.RepaintIfRequested();

            return tree;
        }

        protected override void DrawEditor(int index)
        {
            var selected = this.MenuTree.Selection;
            _scroll = GUILayout.BeginScrollView(_scroll);
            {
                base.DrawEditor(index);
            }
            GUILayout.EndScrollView();

            if (selected.SelectedValue.GetType() == typeof(BaseLevel))
            {
                ShowLevelData(selected);
            }

            if (selected.SelectedValue.GetType() == typeof(BaseBlock))
            {
                ShowBlockData(selected);
            }
        }
        private void ShowLevelData(OdinMenuTreeSelection selected)
        {
            SirenixEditorGUI.HorizontalLineSeparator();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Clear Level", GUILayoutOptions.Height(30)))
                    {
                        var asset = selected.SelectedValue as BaseLevel;
                        if (asset is{})
                        {
                            asset.levelData.Clear();
                            asset.dataTable = new BaseBlockProperties[asset.cols, asset.rows];
                        }
                    }

                    SirenixEditorGUI.VerticalLineSeparator();
                }
                GUILayout.EndHorizontal();

                SirenixEditorGUI.HorizontalLineSeparator();

                if (GUILayout.Button("Delete Level", GUILayoutOptions.Height(30)))
                {
                    var asset = selected.SelectedValue as BaseLevel;
                    var path = AssetDatabase.GetAssetPath(asset);
                    _dataList.baseLevelList.List.RemoveAt(_dataList.baseLevelList.List.IndexOf(asset));
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();

                    Debug.Assert(asset != null, nameof(asset) + " != null");
                    asset.SetToList();
                }
            }
            GUILayout.EndVertical();
        }



        private void ShowBlockData(OdinMenuTreeSelection selected)
        {
            GUILayout.FlexibleSpace();

            SirenixEditorGUI.HorizontalLineSeparator();
            if (!GUILayout.Button("Delete Block", GUILayoutOptions.Height(30)))
                return;
            var asset = selected.SelectedValue as BaseBlock;
            var path = AssetDatabase.GetAssetPath(asset);
            _dataList.baseBlockList.List.RemoveAt(_dataList.baseBlockList.List.IndexOf(asset));
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(
                menuItem.Rect,
                menuItem.Value,
                false,
                false);
        }

        public void SetSelected(int index)
        {
            this._selectedBlock = index > 0 ? this.blockList[index] : null;
        }
    }
}
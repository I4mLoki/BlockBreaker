using System;
using System.Collections.Generic;
using System.Linq;
using DataConfig;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace Editor
{
    public class DataListEditorWindow : EditorWindow
    {
        private GUISkin skin;

        private SelectionMainMenu selectionMainMenu;
        private SelectionCreateMenu selectionCreateMenu;
        private SelectionShowMenu selectionShowMenu;

        private string blockName = "New Block";

        private Texture2D blockIcon;
        private Texture2D background;

        private List<BaseBlockProperties> levelData;

        private BaseLevelList baseLevelList;
        private BaseBlockList baseBlockList;
        private BaseBoosterList baseBoosterList;

        private BaseLevel tempBaseLevel;
        private BaseBlock tempBaseBlock;

        private SerializedObject baseLevelSo;
        private SerializedObject baseBlockSo;
        private SerializedProperty blockHeads;
        private SerializedProperty blockEyes;
        private SerializedProperty blockEars;
        private SerializedProperty blockMouth;
        private SerializedProperty blockHips;
        private SerializedProperty blockArms;
        private SerializedProperty blockLegs;
        private SerializedProperty blockMaterials;

        private bool showBlockEars;
        private bool showBlockMouth;
        private bool showBlockHips;
        private bool showBlockArms;
        private bool showBlockLegs;
        private bool showBlockMaterials;
        private bool showBlockEyes;
        private bool isUnique;
        private bool showBlockHeads;
        private bool mDown;

        private Vector2 scrollPosition;
        private Vector2 scrollPositionBoard;

        private int sizeX = 1;
        private int sizeY = 1;
        private int blockIndex = 1;
        private int levelNumber;
        private int maxRows = 10;
        private int maxCols = 7;
        private int star1Score;
        private int star2Score;
        private int star3Score;
        private int totalBlocks;
        private int brush;
        private int hits;
        private int levelIndex = 1;

        private static string dataListPath = "Assets/Data/DataList.asset";
        public static DataList _target;

        private static readonly GUILayoutOption MiniButtonWidth = GUILayout.Width(20f);

        private static readonly GUIContent
            PrevButtonContent = new GUIContent("\u2190", "move previous");

        private static readonly GUIContent
            NextButtonContent = new GUIContent("\u2192", "move next");

        private static readonly GUIContent
            DeleteButtonContent = new GUIContent("Delete", "delete");

        private static readonly GUIContent
            ClearLevelContent = new GUIContent("\u21ba", "clear level");

        private static int[] levelSquares = new int[175];
        private static int[] blockHits = new int[175];

        [MenuItem("BlockBreaker/Data Editor")]
        private static void ShowWindow()
        {
            if (AssetDatabase.LoadAssetAtPath<DataList>(dataListPath) == null)
                DataListEditor.CreateNewDataList();
            else
                _target = AssetDatabase.LoadAssetAtPath<DataList>(dataListPath);

            var window = GetWindow<DataListEditorWindow>();
            window.titleContent = new GUIContent("Data Editor");
            window.Show();
        }

        private void OnEnable()
        {
            DataListEditor.CheckFolders();
            DataListEditor.CheckLists();

            if (AssetDatabase.LoadAssetAtPath("Assets/Art/GuiStyle/GUISkin.guiskin", typeof(GUISkin)) as GUISkin)
                skin = AssetDatabase.LoadAssetAtPath("Assets/Art/GuiStyle/GUISkin.guiskin", typeof(GUISkin)) as GUISkin;

            baseLevelList = DataListEditor._target.BaseLevelList;
            baseBlockList = DataListEditor._target.BaseBlockList;
            baseBoosterList = DataListEditor._target.BaseBoosterList;

            tempBaseLevel = new BaseLevel();
            baseLevelSo = new SerializedObject(tempBaseLevel);
            levelData = new List<BaseBlockProperties>();
            // levelData = baseLevelSo.FindProperty("levelData");
            tempBaseBlock = new BaseBlock();
            baseBlockSo = new SerializedObject(tempBaseBlock);
            blockHeads = baseBlockSo.FindProperty("blockHeads");
            blockEyes = baseBlockSo.FindProperty("blockEyes");
            blockEars = baseBlockSo.FindProperty("blockEars");
            blockMouth = baseBlockSo.FindProperty("blockMouth");
            blockHips = baseBlockSo.FindProperty("blockHips");
            blockArms = baseBlockSo.FindProperty("blockArms");
            blockLegs = baseBlockSo.FindProperty("blockLegs");
            blockMaterials = baseBlockSo.FindProperty("blockMaterials");
        }

        private void OnGUI()
        {
            GUI.skin = skin;

            GUILayout.BeginVertical();

            {
                GUILayout.Label("DATA EDITOR", GUILayout.Height(30));
            }

            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();

            {
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));

                {
                    if (GUILayout.Toggle(selectionMainMenu == SelectionMainMenu.ShowData, "Show Data", "button",
                        GUILayout.Height(30)))
                        selectionMainMenu = SelectionMainMenu.ShowData;

                    if (GUILayout.Toggle(selectionMainMenu == SelectionMainMenu.CreateData, "Create Data", "button",
                        GUILayout.Height(30)))
                        selectionMainMenu = SelectionMainMenu.CreateData;
                }

                GUILayout.EndVertical();

                if (selectionMainMenu == SelectionMainMenu.ShowData) DrawShowMenu();

                if (selectionMainMenu == SelectionMainMenu.CreateData) DrawCreateMenu();
            }

            GUILayout.EndHorizontal();

            if (!GUI.changed) return;
            EditorUtility.SetDirty(baseLevelList);
            EditorUtility.SetDirty(baseBlockList);
            EditorUtility.SetDirty(baseBoosterList);
        }


        #region CREATION_MENUS

        private void DrawCreateMenu()
        {
            GUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));

            if (GUILayout.Toggle(selectionCreateMenu == SelectionCreateMenu.CreateNewLevel, "Create\n New Level",
                "button",
                GUILayout.Height(50)))
                selectionCreateMenu = SelectionCreateMenu.CreateNewLevel;

            if (GUILayout.Toggle(selectionCreateMenu == SelectionCreateMenu.CreateNewBlock, "Create\n New Block",
                "button",
                GUILayout.Height(50)))
                selectionCreateMenu = SelectionCreateMenu.CreateNewBlock;

            if (GUILayout.Toggle(selectionCreateMenu == SelectionCreateMenu.CreateNewBooster, "Create\n New Booster",
                "button",
                GUILayout.Height(50)))
                selectionCreateMenu = SelectionCreateMenu.CreateNewBooster;

            GUILayout.EndVertical();

            CheckSelectionCreationMenu();
        }

        private void CheckSelectionCreationMenu()
        {
            switch (selectionCreateMenu)
            {
                case SelectionCreateMenu.CreateNewLevel:
                    DrawCreateNewLevelWindow();
                    break;

                case SelectionCreateMenu.CreateNewBlock:
                    DrawCreateNewBlockWindow();
                    break;

                case SelectionCreateMenu.CreateNewBooster:
                    DrawCreateNewBoosterWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawCreateNewLevelWindow()
        {
            GUILayout.BeginVertical("box", GUILayout.MaxWidth(300));
            {
                GUILayout.BeginHorizontal("box");
                {
                    levelNumber = baseLevelList.LevelList.Count + 1;
                    levelNumber = EditorGUILayout.IntField("Level Number", levelNumber);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    maxRows = EditorGUILayout.IntField("Max Rows", maxRows);
                }
                GUILayout.EndHorizontal();

                GUI.enabled = false;
                GUILayout.BeginHorizontal("box");
                {
                    maxCols = EditorGUILayout.IntField("Max Cols", maxCols);
                }
                GUILayout.EndHorizontal();
                GUI.enabled = true;

                GUILayout.BeginHorizontal("box");
                {
                    star1Score = EditorGUILayout.IntField("Star 1 Score", star1Score);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    star2Score = EditorGUILayout.IntField("Star 2 Score", star2Score);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    star3Score = EditorGUILayout.IntField("Star 3 Score", star3Score);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    background =
                        EditorGUILayout.ObjectField("Background", background, typeof(Texture2D), false) as Texture2D;
                }
                GUILayout.EndHorizontal();

                // GUI.enabled = false;
                // GUILayout.BeginHorizontal("box");
                // {
                //     totalBlocks = EditorGUILayout.IntField("Total Blocks", totalBlocks);
                // }
                // GUILayout.EndHorizontal();
                // GUI.enabled = true;

                // GUILayout.BeginVertical();
                // {
                //     DrawGUIBlocks();
                // }
                // GUILayout.EndVertical();

                // scrollPosition = GUILayout.BeginScrollView(scrollPosition, "box", GUILayout.Width(400));
                // {
                //     GUILayout.BeginVertical("box", GUILayout.Width(220), GUILayout.ExpandHeight(false));
                //     {
                //         GUILayout.BeginHorizontal("box");
                //         {
                //             showLevelData = GUILayout.Toggle(showLevelData, "Level Data", "button",
                //                 GUILayout.Width(90));
                //             if (showLevelData)
                //             {
                //                 GUILayout.BeginVertical();
                //                 {
                //                     for (var i = 0; i < levelData.Count; i++)
                //                     {
                //                         EditorGUILayout.LabelField(levelData[i].Block.ToString());
                //                         EditorGUILayout.LabelField(levelData[i].X.ToString());
                //                         EditorGUILayout.LabelField(levelData[i].Y.ToString());
                //                         EditorGUILayout.LabelField(levelData[i].Hits.ToString());
                //                         GUILayout.Space(10);
                //                     }
                //                     // EditorList.Show(levelData,
                //                     //     EditorListOption.Buttons | EditorListOption.ElementLabels);
                //                 }
                //                 GUILayout.EndVertical();
                //             }
                //         }
                //         GUILayout.EndHorizontal();
                //     }
                //     GUILayout.EndVertical();
                // }
                // GUILayout.EndScrollView();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                // {
                //     if (GUILayout.Button("\u2613",
                //         new GUILayoutOption[] { GUILayout.Width(65), GUILayout.Height(65) }))
                //         brush = 0;

                if (GUILayout.Button(ClearLevelContent,
                    new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30) }))
                {
                    ClearLevel();
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Add Level To List", GUILayout.Height(30), GUILayout.Width(150)))
                {
                    DataListEditor.CreateLevelAndAddToList(levelNumber, maxRows, maxCols, totalBlocks, star1Score,
                        star2Score, star3Score, background, levelData);
                }

                // GUI.enabled = false;
                // if (baseBlockList.BlockList.Count > 0)
                //     GUILayout.Label(baseBlockList.BlockList[brush].BlockIcon);
                // GUI.enabled = true;
                // }
                GUILayout.EndHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                {
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            // GUILayout.BeginVertical("box");
            // {
            //     DrawGameField();
            // }
            // GUILayout.EndVertical();

            EditorUtility.SetDirty(baseLevelList);
        }

        private void DrawCreateNewBlockWindow()
        {
            GUILayout.BeginVertical("box", GUILayout.Width(400));
            {
                GUILayout.BeginHorizontal("box");
                {
                    isUnique = Toggle(new GUIContent("Unique", "Should only one block of this type be displayed?"),
                        isUnique, GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    blockName = TextField("Block Name", blockName);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    blockIcon =
                        ObjectField("Block Icon", blockIcon, typeof(Texture2D), false) as Texture2D;
                }
                GUILayout.EndHorizontal();

                scrollPosition = GUILayout.BeginScrollView(scrollPosition, "box", GUILayout.Width(420));
                {
                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockHeads = GUILayout.Toggle(showBlockHeads, "Heads", "button", GUILayout.Width(150));
                        if (showBlockHeads)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockHeads, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockEyes = GUILayout.Toggle(showBlockEyes, "Eyes", "button", GUILayout.Width(150));
                        if (showBlockEyes)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockEyes, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockEars = GUILayout.Toggle(showBlockEars, "Ears", "button", GUILayout.Width(150));
                        if (showBlockEars)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockEars, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockMouth = GUILayout.Toggle(showBlockMouth, "Mouths", "button", GUILayout.Width(150));
                        if (showBlockMouth)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockMouth, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockHips = GUILayout.Toggle(showBlockHips, "Hips", "button", GUILayout.Width(150));
                        if (showBlockHips)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockHips, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockArms = GUILayout.Toggle(showBlockArms, "Arms", "button", GUILayout.Width(150));
                        if (showBlockArms)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockArms, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockLegs = GUILayout.Toggle(showBlockLegs, "Legs", "button", GUILayout.Width(150));
                        if (showBlockLegs)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockLegs, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockMaterials = GUILayout.Toggle(showBlockMaterials, "Materials", "button",
                            GUILayout.Width(150));
                        if (showBlockMaterials)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockMaterials, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();

                GUILayout.Space(10);

                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Size X");
                        sizeX = EditorGUILayout.IntField(sizeX);

                        GUILayout.Space(20);

                        GUILayout.Label("Size Y");
                        sizeY = EditorGUILayout.IntField(sizeY);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                GUILayout.Space(50);

                //Options Buttons
                GUILayout.BeginHorizontal("box");
                {
                    if (GUILayout.Button(ClearLevelContent,
                        new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30) }))
                    {
                        ClearBlockFields();
                    }

                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Add Block To List", GUILayout.Width(150), GUILayout.Height(30)))
                    {
                        DataListEditor.CreateBlockAndAddToList(isUnique, blockName, blockIcon, blockHeads, blockEyes,
                            blockEars, blockMouth, blockHips, blockArms, blockLegs, blockMaterials, sizeX, sizeY);
                        ClearBlockFields();
                    }
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            if (GUI.changed)
                EditorUtility.SetDirty(baseLevelList);
        }

        private void DrawCreateNewBoosterWindow()
        {
            GUILayout.Label("Not Implemented Yet", "box");
        }

        private void ClearLevel()
        {
            tempBaseLevel = new BaseLevel();
            baseLevelSo = new SerializedObject(tempBaseLevel);

            levelNumber = baseLevelList.LevelList.Count + 1;
            maxRows = 10;
            maxCols = 7;
            star1Score = 100;
            star2Score = 300;
            star3Score = 500;
            background = null;
            totalBlocks = 0;
            baseLevelList.LevelList[levelIndex - 1].LevelData.Clear();

            for (var i = 0; i < levelSquares.Length; i++) levelSquares[i] = 0;
            for (var i = 0; i < blockHits.Length; i++) blockHits[i] = 0;
        }

        private void ClearBlockFields()
        {
            tempBaseBlock = new BaseBlock();
            baseBlockSo = new SerializedObject(tempBaseBlock);
            blockHeads = baseBlockSo.FindProperty("blockHeads");
            blockEyes = baseBlockSo.FindProperty("blockEyes");
            blockEars = baseBlockSo.FindProperty("blockEars");
            blockMouth = baseBlockSo.FindProperty("blockMouth");
            blockHips = baseBlockSo.FindProperty("blockHips");
            blockArms = baseBlockSo.FindProperty("blockArms");
            blockLegs = baseBlockSo.FindProperty("blockLegs");
            blockMaterials = baseBlockSo.FindProperty("blockMaterials");

            showBlockArms = false;
            showBlockEars = false;
            showBlockEyes = false;
            showBlockHeads = false;
            showBlockHips = false;
            showBlockLegs = false;
            showBlockMouth = false;
            showBlockMaterials = false;

            isUnique = false;
            blockName = "New Block";
            blockIcon = null;
            blockHeads.ClearArray();
            blockEyes.ClearArray();
            blockEars.ClearArray();
            blockMouth.ClearArray();
            blockHips.ClearArray();
            blockArms.ClearArray();
            blockLegs.ClearArray();
            blockMaterials.ClearArray();
            sizeX = 1;
            sizeY = 1;
        }

        private void DrawGameField(BaseLevel baseLevel)
        {
            MouseControl();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            scrollPositionBoard = GUILayout.BeginScrollView(scrollPositionBoard);
            for (var row = baseLevel.Rows; row > -1; row--)
            {
                GUILayout.BeginHorizontal();
                if (row >= baseLevel.SafeArea)
                    GUI.enabled = true;
                else
                    GUI.enabled = false;
                for (var col = 0; col < baseLevel.Cols; col++)
                {
                    var imageButton = new object();
                    if (levelSquares[row * baseLevel.Cols + col] == 0)
                    {
                        imageButton = "X";
                    }
                    else if (levelSquares[row * baseLevel.Cols + col] != 0)
                    {
                        var index = levelSquares[row * baseLevel.Cols + col];
                        if (baseBlockList.BlockList.Count > index)
                            if (baseBlockList.BlockList[index].BlockIcon != null)
                                imageButton = baseBlockList.BlockList[index].BlockIcon;
                    }

                    BeginVertical("button");
                    if (GUILayout.Button((imageButton) as Texture,
                        new GUILayoutOption[] { GUILayout.Width(60), GUILayout.Height(60) }))
                    {
                    }

                    var tempHits = blockHits[row * baseLevel.Cols + col];
                    tempHits = EditorGUILayout.IntField(tempHits);
                    EndVertical();

                    if (mDown && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) &&
                        row >= baseLevel.SafeArea)
                    {
                        // if(levelSquares[row * baseLevel.Col + col] != brush)
                        SetType(col, row);
                    }
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            UpdateGrid();
        }

        private void SetTypeLevelData(BaseBlock block, int col, int row, int i)
        {
            var tempBlock = new BaseBlockProperties();
            tempBlock.Block = block;
            tempBlock.X = row;
            tempBlock.Y = col;
            tempBlock.Hits = i;

            baseLevelList.LevelList[levelIndex - 1].LevelData.Add(tempBlock);
        }

        private void DrawGUIBlocks()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Blocks:", EditorStyles.boldLabel);
                GUILayout.Space(10);
                ShowItemsPanel();
                hits = EditorGUILayout.IntField("Hits", hits, "box");
            }
            GUILayout.EndHorizontal();
        }

        private void ShowItemsPanel()
        {
            GUILayout.BeginVertical(GUILayout.Width(100));

            var tempContent = baseBlockList.BlockList.Select(block => block.BlockName).ToList();

            var content = tempContent.ToArray();

            brush = EditorGUILayout.Popup("", brush, content);

            GUILayout.EndVertical();
        }

        private void SetType(int col, int row)
        {
            levelSquares[row * baseLevelList.LevelList[levelIndex - 1].Cols + col] = brush;

            var tempList = new List<BaseBlockProperties>();
            tempList = baseLevelList.LevelList[levelIndex - 1].LevelData;

            var index = tempList.FindIndex(i => i.X == row && i.Y == col);
            var check = tempList.Any(i => i.X == row && i.Y == col);

            if (tempList.Count > 0 && brush != 0)
            {
                if (check)
                {
                    tempList.RemoveAt(index);
                }

                if (baseBlockList.BlockList[brush].SizeX == 1 && baseBlockList.BlockList[brush].SizeY == 1)
                    SetTypeLevelData(baseBlockList.BlockList[brush], col, row, hits);
            }
            else if (brush != 0)
            {
                // if (baseBlockList.BlockList[brush].SizeX == 1 && baseBlockList.BlockList[brush].SizeY == 1)
                    SetTypeLevelData(baseBlockList.BlockList[brush], col, row, hits);
                // else
                // {
                //     var i = 0;
                //     while (i < baseBlockList.BlockList[brush].SizeX)
                //     {
                //
                //         SetTypeLevelData(baseBlockList.BlockList[brush], col, row + i, hits);
                //         i++;
                //     }
                //
                //     i = 0;
                //     while (i < baseBlockList.BlockList[brush].SizeY)
                //     {
                //         SetTypeLevelData(baseBlockList.BlockList[brush], col + i, row, hits);
                //         i++;
                //     }
                // }
            }
            else if (brush == 0)
                tempList.RemoveAt(index);

            baseLevelList.LevelList[levelIndex - 1].LevelData = tempList;
        }

        #endregion

        #region SHOWING_MENUS

        private void DrawShowMenu()
        {
            GUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));

            if (GUILayout.Toggle(selectionShowMenu == SelectionShowMenu.ShowLevelData, "Show\n Level",
                "button",
                GUILayout.Height(50)))
                selectionShowMenu = SelectionShowMenu.ShowLevelData;

            if (GUILayout.Toggle(selectionShowMenu == SelectionShowMenu.ShowBlockData, "Show\n Block",
                "button",
                GUILayout.Height(50)))
                selectionShowMenu = SelectionShowMenu.ShowBlockData;

            if (GUILayout.Toggle(selectionShowMenu == SelectionShowMenu.ShowBoosterData, "Show\n Booster",
                "button",
                GUILayout.Height(50)))
                selectionShowMenu = SelectionShowMenu.ShowBoosterData;

            GUILayout.EndVertical();

            CheckSelectionShowMenu();
        }

        private void CheckSelectionShowMenu()
        {
            switch (selectionShowMenu)
            {
                case SelectionShowMenu.ShowLevelData:
                    DrawShowLevelWindow();
                    break;

                case SelectionShowMenu.ShowBlockData:
                    if (baseBlockList.BlockList.Count > 0)
                        DrawShowBlockWindow();
                    else
                        GUILayout.Label("Block List is Empty");
                    break;

                case SelectionShowMenu.ShowBoosterData:
                    DrawShowBoosterWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawShowLevelWindow()
        {
            if (baseLevelList.LevelList.Count == 0) return;
            var level = baseLevelList.LevelList[levelIndex - 1];

            GUILayout.BeginVertical("box", GUILayout.Width(100));
            {
                // GUILayout.FlexibleSpace();
                GUILayout.BeginVertical("box", GUILayout.Width(100));
                {
                    GUILayout.BeginHorizontal("box");
                    {
                        levelIndex = Mathf.Clamp(
                            EditorGUILayout.IntField("Current Level", levelIndex, GUILayout.ExpandWidth(false)), 1,
                            baseLevelList.LevelList.Count);

                        EditorGUILayout.LabelField("of " + baseLevelList.LevelList.Count + " Level ", "",
                            GUILayout.ExpandWidth(false));

                        if (GUILayout.Button(PrevButtonContent, EditorStyles.miniButtonRight, MiniButtonWidth))
                            if (levelIndex > 1)
                            {
                                levelIndex--;
                                UpdateGrid();
                            }

                        GUILayout.Space(5);

                        if (GUILayout.Button(NextButtonContent, EditorStyles.miniButtonRight, MiniButtonWidth))
                            if (levelIndex < baseLevelList.LevelList.Count)
                            {
                                levelIndex++;
                                UpdateGrid();
                            }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal("box");
                {
                    level.LevelNumber = EditorGUILayout.IntField("Level Number", level.LevelNumber);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    level.Rows = EditorGUILayout.IntField("Max Rows", level.Rows);
                }
                GUILayout.EndHorizontal();

                GUI.enabled = false;
                GUILayout.BeginHorizontal("box");
                {
                    level.Cols = EditorGUILayout.IntField("Max Cols", level.Cols);
                }
                GUILayout.EndHorizontal();
                GUI.enabled = true;

                GUILayout.BeginHorizontal("box");
                {
                    level.Star1Score = EditorGUILayout.IntField("Star 1 Score", level.Star1Score);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    level.Star2Score = EditorGUILayout.IntField("Star 2 Score", level.Star2Score);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    level.Star3Score = EditorGUILayout.IntField("Star 3 Score", level.Star3Score);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    level.SafeArea = EditorGUILayout.IntField("Safe Area", level.SafeArea);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    level.Background =
                        EditorGUILayout.ObjectField("Background", level.Background, typeof(Texture2D), false) as
                            Texture2D;
                }
                GUILayout.EndHorizontal();

                GUI.enabled = false;
                GUILayout.BeginHorizontal("box");
                {
                    level.Blocks = level.LevelData.Count;
                    level.Blocks = EditorGUILayout.IntField("Total Blocks", level.Blocks);
                }
                GUILayout.EndHorizontal();
                GUI.enabled = true;

                GUILayout.BeginVertical(GUILayout.Width(50));
                {
                    DrawGUIBlocks();
                }
                GUILayout.EndVertical();

                // scrollPosition = GUILayout.BeginScrollView(scrollPosition, "box");
                {
                    // GUILayout.BeginVertical("box", GUILayout.Width(200), GUILayout.ExpandHeight(false));
                    // {
                    // GUILayout.BeginHorizontal("box");
                    // {
                    //     showLevelData = GUILayout.Toggle(showLevelData, "Level Data", "button",
                    //         GUILayout.Width(90));
                    //     if (showLevelData)
                    //     {
                    //         GUILayout.BeginVertical(GUILayout.Width(100));
                    //         {
                    //             foreach (var t in level.LevelData)
                    //             {
                    //                 show = GUILayout.Toggle(show, level.LevelData.IndexOf(t).ToString(), "button",
                    //                     GUILayout.Width(50));
                    //                 if (show)
                    //                 {
                    //                     EditorGUILayout.LabelField(t.Block.name, "", "box");
                    //                     EditorGUILayout.LabelField(t.X.ToString(), "", "box");
                    //                     EditorGUILayout.LabelField(t.Y.ToString(), "", "box");
                    //                     EditorGUILayout.LabelField(t.Hits.ToString(), "", "box");
                    //                 }
                    //
                    //                 GUILayout.Space(10);
                    //             }
                    //         }
                    //         GUILayout.EndVertical();
                    //     }
                    // }
                    // GUILayout.EndHorizontal();
                    // }
                    // GUILayout.EndVertical();
                }
                // GUILayout.EndScrollView();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("\u2613",
                        new GUILayoutOption[] { GUILayout.Width(65), GUILayout.Height(65) }))
                        brush = 0;

                    if (GUILayout.Button(ClearLevelContent,
                        new GUILayoutOption[] { GUILayout.Width(65), GUILayout.Height(65) }))
                    {
                        ClearLevel();
                    }

                    GUI.enabled = false;
                    if (baseBlockList.BlockList.Count > 0)
                        GUILayout.Label(baseBlockList.BlockList[brush].BlockIcon);
                    GUI.enabled = true;
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginHorizontal("box");
                {
                    // if (GUILayout.Button("Delete Block", GUILayout.Width(150), GUILayout.Height(40)))
                    if (GUILayout.Button(DeleteButtonContent, GUILayout.Width(150), GUILayout.Height(40)))
                    {
                        DeleteLevel(levelIndex - 1);
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            {
                DrawGameField(level);
            }
            GUILayout.EndVertical();

            EditorUtility.SetDirty(baseLevelList);
        }

        private void DrawShowBlockWindow()
        {
            var block = baseBlockList.BlockList[blockIndex - 1];

            GUILayout.BeginVertical("box", GUILayout.Width(400));
            GUILayout.BeginHorizontal("box");
            {
                blockIndex = Mathf.Clamp(
                    EditorGUILayout.IntField("Current Block", blockIndex, GUILayout.ExpandWidth(false)), 1,
                    baseBlockList.BlockList.Count);

                EditorGUILayout.LabelField("of " + baseBlockList.BlockList.Count + " Blocks ", "",
                    GUILayout.ExpandWidth(false));

                if (GUILayout.Button(PrevButtonContent, EditorStyles.miniButtonRight, MiniButtonWidth))
                    if (blockIndex > 1)
                        blockIndex--;

                GUILayout.Space(5);

                if (GUILayout.Button(NextButtonContent, EditorStyles.miniButtonRight, MiniButtonWidth))
                    if (blockIndex < baseBlockList.BlockList.Count)
                        blockIndex++;

                GUILayout.EndHorizontal();
            }

            {
                GUILayout.BeginHorizontal("box");
                {
                    block.ShouldBeUnique = Toggle(
                        new GUIContent("Unique", "Should only one block of this type be displayed?"),
                        block.ShouldBeUnique, GUILayout.ExpandWidth(false));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    block.BlockName = TextField("Block Name",
                        block.BlockName);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box");
                {
                    block.BlockIcon =
                        ObjectField("Block Icon", block.BlockIcon, typeof(Texture2D),
                            false) as Texture2D;
                }
                GUILayout.EndHorizontal();

                scrollPosition = GUILayout.BeginScrollView(scrollPosition, "box", GUILayout.Width(420));
                {
                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockHeads = GUILayout.Toggle(showBlockHeads, "Heads", "button", GUILayout.Width(150));
                        if (showBlockHeads)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockHeads, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockEyes = GUILayout.Toggle(showBlockEyes, "Eyes", "button", GUILayout.Width(150));
                        if (showBlockEyes)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockEyes, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockEars = GUILayout.Toggle(showBlockEars, "Ears", "button", GUILayout.Width(150));
                        if (showBlockEars)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockEars, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockMouth = GUILayout.Toggle(showBlockMouth, "Mouths", "button", GUILayout.Width(150));
                        if (showBlockMouth)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockMouth, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockHips = GUILayout.Toggle(showBlockHips, "Hips", "button", GUILayout.Width(150));
                        if (showBlockHips)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockHips, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockArms = GUILayout.Toggle(showBlockArms, "Arms", "button", GUILayout.Width(150));
                        if (showBlockArms)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockArms, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockLegs = GUILayout.Toggle(showBlockLegs, "Legs", "button", GUILayout.Width(150));
                        if (showBlockLegs)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockLegs, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal("box");
                    {
                        showBlockMaterials = GUILayout.Toggle(showBlockMaterials, "Materials", "button",
                            GUILayout.Width(150));
                        if (showBlockMaterials)
                        {
                            GUILayout.BeginVertical();
                            {
                                EditorList.Show(blockMaterials, EditorListOption.Buttons);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();

                GUILayout.Space(10);

                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Size X");
                        block.SizeX = EditorGUILayout.IntField(block.SizeX);

                        GUILayout.Space(20);

                        GUILayout.Label("Size Y");
                        block.SizeY = EditorGUILayout.IntField(block.SizeY);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                GUILayout.Space(50);

                //Options Buttons
                GUILayout.BeginHorizontal("box");
                {
                    // if (GUILayout.Button("Delete Block", GUILayout.Width(150), GUILayout.Height(40)))
                    if (GUILayout.Button(DeleteButtonContent, GUILayout.Width(150), GUILayout.Height(40)))
                    {
                        DeleteBlock(blockIndex - 1);
                    }
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            if (GUI.changed)
                baseBlockSo.ApplyModifiedProperties();
        }

        private void DrawShowBoosterWindow()
        {
        }

        #endregion

        #region TOOLS

        private void DeleteLevel(int i)
        {
            var pathSo =
                AssetDatabase.GetAssetPath(baseLevelList.LevelList[i]);

            levelIndex = 1;
            AssetDatabase.DeleteAsset(pathSo);
            baseLevelList.LevelList.RemoveAt(i);
        }

        private void UpdateGrid()
        {
            for (var i = 0; i < levelSquares.Length; i++)
            {
                levelSquares[i] = 0;
            }

            for (var i = 0; i < blockHits.Length; i++)
            {
                blockHits[i] = 0;
            }

            foreach (var block in baseLevelList.LevelList[levelIndex - 1].LevelData)
            {
                levelSquares[block.X * baseLevelList.LevelList[levelIndex - 1].Cols + block.Y] =
                    baseBlockList.BlockList.IndexOf(block.Block);

                blockHits[block.X * baseLevelList.LevelList[levelIndex - 1].Cols + block.Y] =
                    block.Hits;
            }
        }

        private void DeleteBlock(int i)
        {
            var pathPrefab = AssetDatabase.GetAssetPath(baseBlockList.BlockList[i].BlockPrefab);
            var pathSo =
                AssetDatabase.GetAssetPath(baseBlockList.BlockList[i]);

            blockIndex = 1;
            AssetDatabase.DeleteAsset(pathSo);
            AssetDatabase.DeleteAsset(pathPrefab);
            baseBlockList.BlockList.RemoveAt(i);
        }

        private void MouseControl()
        {
            if (Event.current.type == EventType.MouseDown)
                mDown = true;
            else if (Event.current.type == EventType.MouseUp) mDown = false;
        }

        [Flags]
        public enum EditorListOption
        {
            None = 0,
            ListSize = 1,
            ListLabel = 2,
            ElementLabels = 4,
            Buttons = 8,
            Default = ListSize | ListLabel | ElementLabels,
            NoElementLabels = ListSize | ListLabel,
            All = Default | Buttons
        }

        public static class EditorList
        {
            private static readonly GUIContent
                MoveButtonContent = new GUIContent("\u21b4", "move down");

            private static readonly GUIContent
                DuplicateButtonContent = new GUIContent("+", "duplicate");

            private static readonly GUIContent
                DeleteButtonContent = new GUIContent("-", "delete");

            private static readonly GUIContent
                AddButtonContent = new GUIContent("+", "add element");

            private static readonly GUILayoutOption MiniButtonWidth = GUILayout.Width(20f);

            public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
            {
                if (!list.isArray)
                {
                    EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                    return;
                }

                bool
                    showListLabel = (options & EditorListOption.ListLabel) != 0,
                    showListSize = (options & EditorListOption.ListSize) != 0;

                if (showListLabel)
                {
                    EditorGUILayout.PropertyField(list);
                    EditorGUI.indentLevel += 1;
                }

                if (!showListLabel || list.isExpanded)
                {
                    SerializedProperty size = list.FindPropertyRelative("Array.size");
                    if (showListSize)
                    {
                        EditorGUILayout.PropertyField(size);
                    }

                    if (size.hasMultipleDifferentValues)
                    {
                        EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
                    }
                    else
                    {
                        ShowElements(list, options);
                    }
                }

                if (showListLabel)
                {
                    EditorGUI.indentLevel -= 1;
                }
            }

            private static void ShowElements(SerializedProperty list, EditorListOption options)
            {
                bool
                    showElementLabels = (options & EditorListOption.ElementLabels) != 0,
                    showButtons = (options & EditorListOption.Buttons) != 0;

                for (var i = 0; i < list.arraySize; i++)
                {
                    if (showButtons)
                    {
                        EditorGUILayout.BeginHorizontal();
                    }

                    if (showElementLabels)
                    {
                        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
                    }

                    if (showButtons)
                    {
                        ShowButtons(list, i);
                        EditorGUILayout.EndHorizontal();
                    }
                }

                if (showButtons && list.arraySize == 0 && GUILayout.Button(AddButtonContent, EditorStyles.miniButton))
                {
                    list.arraySize += 1;
                }
            }

            private static void ShowButtons(SerializedProperty list, int index)
            {
                if (GUILayout.Button(MoveButtonContent, EditorStyles.miniButtonLeft, MiniButtonWidth))
                {
                    list.MoveArrayElement(index, index + 1);
                }

                if (GUILayout.Button(DuplicateButtonContent, EditorStyles.miniButtonMid, MiniButtonWidth))
                {
                    list.InsertArrayElementAtIndex(index);
                }

                if (GUILayout.Button(DeleteButtonContent, EditorStyles.miniButtonRight, MiniButtonWidth))
                {
                    var oldSize = list.arraySize;
                    list.DeleteArrayElementAtIndex(index);
                    if (list.arraySize == oldSize)
                    {
                        list.DeleteArrayElementAtIndex(index);
                    }
                }
            }
        }

        private enum SelectionMainMenu
        {
            ShowData,
            CreateData
        }

        private enum SelectionCreateMenu
        {
            CreateNewLevel,
            CreateNewBlock,
            CreateNewBooster
        }

        private enum SelectionShowMenu
        {
            ShowLevelData,
            ShowBlockData,
            ShowBoosterData
        }

        #endregion
    }
}
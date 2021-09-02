using System;
using System.Collections.Generic;
using System.IO;
using DataConfig;
using Editor;
using Gameplay;
using PlasticPipe.Server;
using Sirenix.OdinInspector.Editor;
using Temporal.Code.DataConfig.BaseObjects;
using TMPro;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using Directory = UnityEngine.Windows.Directory;
using Object = UnityEngine.Object;

namespace Editor
{
    [CustomEditor(typeof(DataList))]
    public class DataListEditor : OdinEditor
    {
        public static DataList _target;

        private static int levelIndex;
        private static int boosterIndex;
        private static int blockIndex;
        private static string blockListPath = "Assets/Data/BaseBlockList.asset";
        private static string dataPaths = "Assets/Data/DataPath.asset";
        private static string boosterListPath = "Assets/Data/BaseBoosterList.asset";
        private static string levelListPath = "Assets/Data/BaseLevelList.asset";
        private static string dataListPath = "Assets/Data/DataList.asset";


        private void OnEnable()
        {
            _target = (DataList)target;
            CheckLists();
        }


        public override void OnInspectorGUI()
        {
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }

        public static void CreateBoosterAndAddToList()
        {
        }

        #region CREATE_LIST

        public static void CheckFolders()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Data"))
                AssetDatabase.CreateFolder("Assets", "Data");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                AssetDatabase.CreateFolder("Assets", "Prefabs");

            if (!AssetDatabase.IsValidFolder("Assets/Art"))
                AssetDatabase.CreateFolder("Assets", "Art");

            if (!AssetDatabase.IsValidFolder("Assets/Data/Blocks"))
                AssetDatabase.CreateFolder("Assets/Data", "Blocks");

            if (!AssetDatabase.IsValidFolder("Assets/Data/Levels"))
                AssetDatabase.CreateFolder("Assets/Data", "Levels");

            if (!AssetDatabase.IsValidFolder("Assets/Data/Boosters"))
                AssetDatabase.CreateFolder("Assets/Data", "Boosters");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/Blocks"))
                AssetDatabase.CreateFolder("Assets/Prefabs", "Blocks");

            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/Boosters"))
                AssetDatabase.CreateFolder("Assets/Prefabs", "Boosters");
        }

        public static void CheckLists()
        {
            if (AssetDatabase.LoadAssetAtPath<DataList>(dataListPath) == null)
                CreateNewDataList();
            else
                _target = AssetDatabase.LoadAssetAtPath<DataList>(dataListPath);

            if (AssetDatabase.LoadAssetAtPath<BaseLevelList>(levelListPath) == null)
                CreateNewLevelList();
            else
                _target.baseLevelList = AssetDatabase.LoadAssetAtPath<BaseLevelList>(levelListPath);

            if (AssetDatabase.LoadAssetAtPath<BaseBlockList>(blockListPath) == null)
                CreateNewBlockList();
            else
                _target.baseBlockList = AssetDatabase.LoadAssetAtPath<BaseBlockList>(blockListPath);

            if (AssetDatabase.LoadAssetAtPath<BaseBoosterList>(boosterListPath) == null)
                CreateNewBoosterList();
            else
                _target.baseBoosterList = AssetDatabase.LoadAssetAtPath<BaseBoosterList>(boosterListPath);


            // if (AssetDatabase.LoadAssetAtPath<DataList>(_target.DataPath.dataListPath) == null)
            //     CreateNewDataList();
            // else
            //     _target = AssetDatabase.LoadAssetAtPath<DataList>(_target.DataPath.dataListPath);
            //
            // if (AssetDatabase.LoadAssetAtPath<BaseLevelList>(_target.DataPath.levelListPath) == null)
            //     CreateNewLevelList();
            // else
            //     _target.BaseLevelList = AssetDatabase.LoadAssetAtPath<BaseLevelList>(_target.DataPath.levelListPath);
            //
            // if (AssetDatabase.LoadAssetAtPath<BaseBlockList>(_target.DataPath.blockListPath) == null)
            //     CreateNewBlockList();
            // else
            //     _target.BaseBlockList = AssetDatabase.LoadAssetAtPath<BaseBlockList>(_target.DataPath.blockListPath);
            //
            // if (AssetDatabase.LoadAssetAtPath<BaseBoosterList>(_target.DataPath.boosterListPath) == null)
            //     CreateNewBoosterList();
            // else
            //     _target.BaseBoosterList = AssetDatabase.LoadAssetAtPath<BaseBoosterList>(_target.DataPath.boosterListPath);
        }

        public static void CreateNewDataList()
        {
            _target = CreateDataList.Create();
            if (!_target) return;
            _target = new DataList();
            dataListPath = AssetDatabase.GetAssetPath(_target);
        }

        private static void CreateNewLevelList()
        {
            levelIndex = 1;
            _target.baseLevelList = CreateLevelList.Create();
            if (!_target.baseLevelList) return;
            _target.baseLevelList.List = new List<BaseLevel>();
            levelListPath = AssetDatabase.GetAssetPath(_target.baseLevelList);
        }

        private static void CreateNewBlockList()
        {
            blockIndex = 1;
            _target.baseBlockList = CreateBlockList.Create();
            if (!_target.baseBlockList) return;
            _target.baseBlockList.List = new List<BaseBlock>();
            _target.dataPath.blockListPath = AssetDatabase.GetAssetPath(_target.baseBlockList);
        }

        private static void CreateNewBoosterList()
        {
            boosterIndex = 1;
            _target.baseBoosterList = CreateBoosterList.Create();
            if (!_target.baseBoosterList) return;
            _target.baseBoosterList.BaseBooster = new List<BaseBooster>();
            _target.dataPath.boosterListPath = AssetDatabase.GetAssetPath(_target.baseBoosterList);
        }

        #endregion

        public static void CreateLevelAndAddToList(int levelNumber, int rows, int col, int blocks, int star1Score,
            int star2Score, int star3Score, Texture2D background, List<BaseBlockProperties> levelData)
        {
            //Check Directory
            if (_target == null)
                _target = AssetDatabase.LoadAssetAtPath<DataList>(EditorPrefs.GetString("DataPathList"));

            //Set Fields
            var tempBaseLevel = CreateInstance<BaseLevel>();
            tempBaseLevel.levelNumber = levelNumber;
            // tempBaseLevel.rows = rows;
            // tempBaseLevel.cols = col;
            tempBaseLevel.blocks = blocks;
            tempBaseLevel.star1Score = star1Score;
            tempBaseLevel.star2Score = star2Score;
            tempBaseLevel.star3Score = star3Score;
            tempBaseLevel.background = background;
            tempBaseLevel.levelData = new List<BaseBlockProperties>();
            tempBaseLevel.levelData = levelData;

            //Create ScriptableObject in Directory
            AssetDatabase.CreateAsset(tempBaseLevel,
                "Assets/Data/Levels/" + ("Level") + "_" + levelNumber + ".asset");

            //Add Block To List
            _target.baseLevelList.List.Add(tempBaseLevel);
        }

        public static void CreateBlockAndAddToList(bool isUnique, string blockName,
            Texture2D blockIcon, SerializedProperty blockHeads, SerializedProperty blockEyes,
            SerializedProperty blockEars, SerializedProperty blockMouth, SerializedProperty blockHips,
            SerializedProperty blockArms, SerializedProperty blockLegs, SerializedProperty blockMaterials, int sizeX,
            int sizeY)
        {
            //Check Directory
            if (_target == null)
                _target = AssetDatabase.LoadAssetAtPath<DataList>(EditorPrefs.GetString("DataPathList"));

            //Set Fields
            var tempBaseBlock = CreateInstance<BaseBlock>();
            tempBaseBlock.blockName = blockName;
            tempBaseBlock.shouldBeUnique = isUnique;
            tempBaseBlock.blockIcon = blockIcon;
            tempBaseBlock.sizeX = sizeX;
            tempBaseBlock.sizeY = sizeY;
            tempBaseBlock.blockArms = SetPropertyToTextureList(blockArms);
            tempBaseBlock.blockEars = SetPropertyToTextureList(blockEars);
            tempBaseBlock.blockHeads = SetPropertyToTextureList(blockHeads);
            tempBaseBlock.blockEyes = SetPropertyToTextureList(blockEyes);
            tempBaseBlock.blockMouth = SetPropertyToTextureList(blockMouth);
            tempBaseBlock.blockHips = SetPropertyToTextureList(blockHips);
            tempBaseBlock.blockLegs = SetPropertyToTextureList(blockLegs);
            tempBaseBlock.blockMaterials = SetPropertyToMaterialList(blockMaterials);

            //Create ScriptableObject in Directory
            AssetDatabase.CreateAsset(tempBaseBlock,
                "Assets/Data/Blocks/" + (_target.baseBlockList.List.Count + 1) + "_" + blockName + ".asset");

            //Add Block To List
            _target.baseBlockList.List.Add(tempBaseBlock);

            //Create GameObject and its components in Scene to Set Prefab
            var go = new GameObject("New Block");

            var text = new GameObject("Text");
            text.transform.parent = go.transform;
            text.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            var blockParts = ObjectFactory.AddComponent<BlockParts>(go);
            blockParts.BaseBlock = tempBaseBlock;

            //Add Block Script
            ObjectFactory.AddComponent<Block>(go);

            //Add Block Script
            var textTemp = ObjectFactory.AddComponent<TextMeshPro>(text);
            textTemp.alignment = TextAlignmentOptions.Center;
            textTemp.autoSizeTextContainer = true;
            textTemp.color = Color.white;

            //Add SpriteRendererComponent
            ObjectFactory.AddComponent<SpriteRenderer>(go);

            //Add BoxCollider and PhysicsMaterial
            // ObjectFactory.AddComponent<BoxCollider2D>(go).sharedMaterial =
            //     AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/Temporal/Materials/Bouncy.physicsMaterial2D");

            var art = new GameObject("Art");
            art.transform.parent = go.transform;

            var head = new GameObject("head");
            var leftEye = new GameObject("leftEye");
            var rightEye = new GameObject("rightEye");
            var leftEar = new GameObject("leftEar");
            var rightEar = new GameObject("rightEar");
            var mouth = new GameObject("mouth");
            var hip = new GameObject("hip");
            var leftArm = new GameObject("leftArm");
            var rightArm = new GameObject("rightArm");
            var leftLeg = new GameObject("leftLeg");
            var rightLeg = new GameObject("rightLeg");

            SpriteRenderer headImage = ObjectFactory.AddComponent<SpriteRenderer>(head);
            SpriteRenderer leftEyeImage = ObjectFactory.AddComponent<SpriteRenderer>(leftEye);
            SpriteRenderer rightEyeImage = ObjectFactory.AddComponent<SpriteRenderer>(rightEye);
            SpriteRenderer leftEarImage = ObjectFactory.AddComponent<SpriteRenderer>(leftEar);
            SpriteRenderer rightEarImage = ObjectFactory.AddComponent<SpriteRenderer>(rightEar);
            SpriteRenderer mouthImage = ObjectFactory.AddComponent<SpriteRenderer>(mouth);
            SpriteRenderer hipImage = ObjectFactory.AddComponent<SpriteRenderer>(hip);
            SpriteRenderer leftArmImage = ObjectFactory.AddComponent<SpriteRenderer>(leftArm);
            SpriteRenderer rightArmImage = ObjectFactory.AddComponent<SpriteRenderer>(rightArm);
            SpriteRenderer leftLegImage = ObjectFactory.AddComponent<SpriteRenderer>(leftLeg);
            SpriteRenderer rightLegImage = ObjectFactory.AddComponent<SpriteRenderer>(rightLeg);

            blockParts.Head = headImage;
            blockParts.LeftEye = leftEyeImage;
            blockParts.RightEye = rightEyeImage;
            blockParts.LeftEar = leftEarImage;
            blockParts.RightEar = rightEarImage;
            blockParts.LeftArm = leftArmImage;
            blockParts.RightArm = rightArmImage;
            blockParts.LeftLeg = leftLegImage;
            blockParts.RightLeg = rightLegImage;
            blockParts.Mouth = mouthImage;
            blockParts.Hip = hipImage;

            head.transform.parent = art.transform;
            leftEye.transform.parent = art.transform;
            rightEye.transform.parent = art.transform;
            leftEar.transform.parent = art.transform;
            rightEar.transform.parent = art.transform;
            leftArm.transform.parent = art.transform;
            rightArm.transform.parent = art.transform;
            leftLeg.transform.parent = art.transform;
            rightLeg.transform.parent = art.transform;
            mouth.transform.parent = art.transform;
            hip.transform.parent = art.transform;

            var prefab = PrefabUtility.SaveAsPrefabAsset(go,
                "Assets/Prefabs/Blocks/" + (_target.baseBlockList.List.Count + 1) + "_" + blockName + ".prefab");

            tempBaseBlock.blockPrefab = prefab;

            DestroyImmediate(go);
        }

        #region TOOLS

        public static List<Texture2D> SetPropertyToTextureList(SerializedProperty list)
        {
            var temp = new List<Texture2D>();
            for (var i = 0; i < list.arraySize; i++)
            {
                var data = list.GetArrayElementAtIndex(i).objectReferenceValue as Texture2D;
                temp.Add(data);
            }

            return temp;
        }

        public static List<Material> SetPropertyToMaterialList(SerializedProperty list)
        {
            var temp = new List<Material>();
            for (var i = 0; i < list.arraySize; i++)
            {
                var data = list.GetArrayElementAtIndex(i).objectReferenceValue as Material;
                temp.Add(data);
            }

            return temp;
        }

        #endregion
    }
}
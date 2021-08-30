using DataConfig;
using I2.Loc;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Temporal.Code.Editor
{
    public class DataEditorWindow : OdinMenuEditorWindow
    {
        private static string blockListPath = "Assets/Data/BaseBlockList.asset";
        private static string boosterListPath = "Assets/Data/BaseBoosterList.asset";
        private static string levelListPath = "Assets/Data/BaseLevelList.asset";
        private static string dataListPath = "Assets/Data/DataList.asset";

        [MenuItem("BlockBreaker/OdinTest")]
        private static void OpenWindow()
        {
            GetWindow<DataEditorWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree { { "Create Data", new CreateNewMenuData() } };

            tree.AddAssetAtPath("Data List",dataListPath);

            return tree;
        }
    }

    public class CreateNewMenuData
    {
        public CreateNewMenuData()
        {
            var blockData = ScriptableObject.CreateInstance<BaseBlock>();
            blockData.name = "New Block Data";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Foldout)]
        public BaseBlock baseBlock;

        [Button("Add New Block SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(baseBlock, "Assets/Data/Blocks" + baseBlock.name + ".asset");
            AssetDatabase.SaveAssets();

            baseBlock = ScriptableObject.CreateInstance<BaseBlock>();
            baseBlock.name = "New Block Data";
        }
    }
}


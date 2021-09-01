using DataConfig;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
namespace Temporal.Code.Editor
{
    public class CreateBlockData
    {
        private DataList _dataList;

        [InlineEditor(Expanded = true)]
        public BaseBlock blockData;
        public CreateBlockData(DataList dataList)
        {
            blockData = ScriptableObject.CreateInstance<BaseBlock>();
            blockData.blockName = "Block " + (dataList.baseBlockList.List.Count + 1);
            _dataList = dataList;
        }

        [Button("Add New Block")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(blockData,_dataList.dataPath.blocksPath + "/" + blockData.blockName + ".asset");
            AssetDatabase.SaveAssets();
            _dataList.baseBlockList.List.Add(blockData);
        }
    }
    public class ShowBlockData : OdinEditor
    {
        private DataList _dataList;

        [InlineEditor(Expanded = true)]
        public BaseBlock blockData;
        public ShowBlockData(DataList dataList)
        {
            blockData = ScriptableObject.CreateInstance<BaseBlock>();
            blockData.blockName = "Block " + (dataList.baseBlockList.List.Count + 1);
            _dataList = dataList;
        }

        [Button("Delete Block")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(blockData,_dataList.dataPath.blocksPath + "/" + blockData.blockName + ".asset");
            AssetDatabase.SaveAssets();
            _dataList.baseBlockList.List.Add(blockData);
        }
    }
}
using DataConfig;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace Temporal.Code.Editor
{
    public class CreateLevelData
    {
        private DataList _dataList;

        [InlineEditor(Expanded = true)]
        public BaseLevel levelData;

        public CreateLevelData(DataList dataList)
        {
            levelData = ScriptableObject.CreateInstance<BaseLevel>();
            levelData.levelNumber = dataList.baseLevelList.List.Count + 1;
            levelData.star1Score = 300;
            levelData.star2Score = 600;
            levelData.star3Score = 900;
            _dataList = dataList;
        }

        [Button("Add New Level")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(levelData,_dataList.dataPath.levelsPath + "/Level " + levelData.levelNumber + ".asset");
            AssetDatabase.SaveAssets();
            _dataList.baseLevelList.List.Add(levelData);
        }
    }
}
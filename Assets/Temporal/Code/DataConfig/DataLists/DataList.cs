using Sirenix.OdinInspector;
using UnityEditor;
namespace Code.DataConfig.DataLists
{
    public class DataList : SerializedScriptableObject
    {
        [BoxGroup("Paths")]
        public DataPath dataPath;

        [BoxGroup("Lists"), ColorBox]
        public BaseLevelList baseLevelList;

        [BoxGroup("Lists"), ColorBox]
        public BaseBlockList baseBlockList;

        [BoxGroup("Lists"), ColorBox]
        public BaseBoosterList baseBoosterList;

        [BoxGroup("Lists"), ColorBox]
        public BaseCharacterList baseCharacterList;

        [BoxGroup("Lists"), ColorBox]
        public VariableDataConfig variableData;

        [OnInspectorInit]
        private void SetLists()
        {
            if (baseLevelList == null)
                baseLevelList = AssetDatabase.LoadAssetAtPath<BaseLevelList>(dataPath.levelListPath);

            if (baseBlockList == null)
                baseBlockList = AssetDatabase.LoadAssetAtPath<BaseBlockList>(dataPath.blockListPath);

            if (baseBoosterList == null)
                baseBoosterList = AssetDatabase.LoadAssetAtPath<BaseBoosterList>(dataPath.boosterListPath);

            if (variableData == null)
                variableData = AssetDatabase.LoadAssetAtPath<VariableDataConfig>(dataPath.variableDataListPath);

            if (baseCharacterList == null)
                baseCharacterList = AssetDatabase.LoadAssetAtPath<BaseCharacterList>(dataPath.characterListPath);
        }
    }
}
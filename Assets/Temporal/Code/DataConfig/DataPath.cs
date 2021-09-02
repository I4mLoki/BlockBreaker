using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
namespace DataConfig
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    [InlineEditor]
    public class DataPath : ScriptableObject
    {
        [BoxGroup("Data Paths")]
        [BoxGroup("Data Paths/Lists"), ColorBox, Sirenix.OdinInspector.FilePath(Extensions = "asset")]
        public string blockListPath = "Assets/Data/BaseBlockList.asset";
        [BoxGroup("Data Paths/Lists"), ColorBox, Sirenix.OdinInspector.FilePath(Extensions = "asset")]
        public string boosterListPath = "Assets/Data/BaseBoosterList.asset";
        [BoxGroup("Data Paths/Lists"), ColorBox, Sirenix.OdinInspector.FilePath(Extensions = "asset")]
        public string levelListPath = "Assets/Data/BaseLevelList.asset";
        [BoxGroup("Data Paths/Lists"), ColorBox, Sirenix.OdinInspector.FilePath(Extensions = "asset")]
        public string dataListPath = "Assets/Data/DataList.asset";
        [BoxGroup("Data Paths/Objects"), ColorBox, FolderPath]
        public string blocksPath = "Assets/Data/Blocks/";
        [BoxGroup("Data Paths/Objects"), ColorBox, FolderPath]
        public string levelsPath = "Assets/Data/levels/";
        [BoxGroup("Data Paths/Objects"), ColorBox, FolderPath]
        public string prefabPath = "Assets/Prefabs/Blocks/";
        [BoxGroup("Data Paths/Objects"), ColorBox, FolderPath]
        public string prefabTemplate;
    }
}
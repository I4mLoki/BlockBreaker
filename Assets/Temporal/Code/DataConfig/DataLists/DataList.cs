using Sirenix.OdinInspector;
using UnityEngine;

namespace DataConfig
{
    public class DataList : SerializedScriptableObject
    {
        public DataPath dataPath;
        public BaseLevelList baseLevelList;
        public BaseBlockList baseBlockList;
        public BaseBoosterList baseBoosterList;
    }
}
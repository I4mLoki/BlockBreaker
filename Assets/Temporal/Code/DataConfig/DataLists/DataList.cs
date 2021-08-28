using UnityEngine;

namespace DataConfig
{
    public class DataList : ScriptableObject
    {
        [SerializeField] private BaseLevelList baseLevelList;
        [SerializeField] private BaseBlockList baseBlockList;
        [SerializeField] private BaseBoosterList baseBoosterList;

        public BaseLevelList BaseLevelList
        {
            get => baseLevelList;
            set => baseLevelList = value;
        }

        public BaseBlockList BaseBlockList
        {
            get => baseBlockList;
            set => baseBlockList = value;
        }

        public BaseBoosterList BaseBoosterList
        {
            get => baseBoosterList;
            set => baseBoosterList = value;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace DataConfig
{
    public class BaseLevelList : ScriptableObject
    {
        [SerializeField] private List<BaseLevel> levelList;

        public List<BaseLevel> LevelList
        {
            get => levelList;
            set => levelList = value;
        }
    }
}
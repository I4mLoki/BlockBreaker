using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    public class BaseLevelList : ScriptableObject
    {
        [SerializeField] private List<BaseLevel> list;

        public List<BaseLevel> List
        {
            get => list;
            set => list = value;
        }
    }
}
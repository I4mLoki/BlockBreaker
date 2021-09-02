using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    public class BaseBlockList : ScriptableObject
    {
        [SerializeField] private List<BaseBlock> list;

        public List<BaseBlock> List
        {
            get => list;
            set => list = value;
        }
    }
}
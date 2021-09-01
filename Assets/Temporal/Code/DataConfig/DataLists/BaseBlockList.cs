using System.Collections.Generic;
using UnityEngine;

namespace DataConfig
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
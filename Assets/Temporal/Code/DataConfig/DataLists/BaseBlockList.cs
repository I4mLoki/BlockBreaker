using System.Collections.Generic;
using UnityEngine;

namespace DataConfig
{
    public class BaseBlockList : ScriptableObject
    {
        [SerializeField] private List<BaseBlock> blockList;

        public List<BaseBlock> BlockList
        {
            get => blockList;
            set => blockList = value;
        }
    }
}
using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    [CreateAssetMenu(menuName = "BlockBreaker/Data/Create BaseBlockList", fileName = "BaseBlockList", order = 0)]
    public class BaseBlockList : ScriptableObject
    {
        [ColorBox]
        public List<BaseBlock> list;
    }
}
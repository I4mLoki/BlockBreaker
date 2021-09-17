using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    [CreateAssetMenu(menuName = "BlockBreaker/Data/Create BaseLevelList", fileName = "BaseLevelList", order = 0)]
    public class BaseLevelList : ScriptableObject
    {
        [ColorBox]
        public List<BaseLevel> list;
    }
}
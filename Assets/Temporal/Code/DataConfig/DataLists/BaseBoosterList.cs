using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    [CreateAssetMenu(menuName = "BlockBreaker/Data/Create BaseBoosterList", fileName = "BaseBoosterList", order = 0)]
    public class BaseBoosterList : ScriptableObject
    {
        [ColorBox]
        public List<BaseBooster> list;
    }
}
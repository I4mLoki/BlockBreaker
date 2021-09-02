using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    public class BaseBoosterList : ScriptableObject
    {
        [SerializeField] private List<BaseBooster> baseBooster;

        public List<BaseBooster> BaseBooster
        {
            get => baseBooster;
            set => baseBooster = value;
        }
    }
}
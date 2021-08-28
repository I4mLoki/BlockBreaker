using System.Collections.Generic;
using UnityEngine;

namespace DataConfig
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
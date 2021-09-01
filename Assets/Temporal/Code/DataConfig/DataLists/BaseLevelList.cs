using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace DataConfig
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
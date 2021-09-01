using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataConfig
{
    [Serializable]
    public class BaseBlockProperties
    {
        public int x;
        public int y;
        [ProgressBar(-1, 300), ShowInInspector]
        public int hits;
    }
}
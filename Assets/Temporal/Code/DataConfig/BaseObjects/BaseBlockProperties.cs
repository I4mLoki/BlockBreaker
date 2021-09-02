using System;
using Sirenix.OdinInspector;
using Temporal.Code.DataConfig.BaseObjects;
using UnityEngine;

namespace DataConfig
{
    [Serializable]
    public struct BaseBlockProperties
    {
        [SuppressInvalidAttributeError]
        public BaseBlock block;
        public int x;
        public int y;
        [ProgressBar(-1, 1000), ShowInInspector]
        public int hits;
    }
}
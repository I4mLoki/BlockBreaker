using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Code.DataConfig.BaseObjects
{
    [Serializable]
    public struct BaseBlockProperties
    {
        public BaseBlock block;
        public int x;
        public int y;
        [ProgressBar(-1, 1000)]
        public int hits;
    }
}
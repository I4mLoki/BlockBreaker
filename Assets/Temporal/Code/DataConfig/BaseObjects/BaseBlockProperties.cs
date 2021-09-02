using System;
using Sirenix.OdinInspector;
namespace Code.DataConfig.BaseObjects
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
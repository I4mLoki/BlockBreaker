using DataConfig;
#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using System;

    [Serializable]
    public struct ItemSlot
    {
        public int hits;
        public BaseBlock Item;
    }
}
#endif
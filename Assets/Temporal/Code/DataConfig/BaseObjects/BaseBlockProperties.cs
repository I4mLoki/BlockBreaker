using System;
using UnityEngine;

namespace DataConfig
{
    [Serializable]
    public class BaseBlockProperties
    {
        [SerializeField] private BaseBlock block;
        [SerializeField] private int x;
        [SerializeField] private int y;
        [SerializeField] private int hits;
        [SerializeField] private int seed;

        public BaseBlock Block
        {
            get => block;
            set => block = value;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public int Hits
        {
            get => hits;
            set => hits = value;
        }

        public int Seed
        {
            get => seed;
            set => seed = value;
        }
    }
}
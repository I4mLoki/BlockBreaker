using DataConfig.Tools;
using UnityEngine;

namespace DataConfig
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private BaseLevel baseLevel;
        [SerializeField] private int cellSize = 50;
        [SerializeField] private Transform blockParent;

        private int cols;
        private int rows;
        private int[,] grid;

        public BaseLevel BaseLevel
        {
            get => baseLevel;
            set => baseLevel = value;
        }

        public int CellSize
        {
            get => cellSize;
            set => cellSize = value;
        }

        public Transform BlockParent
        {
            get => blockParent;
            set => blockParent = value;
        }

        public void SetComponents(BaseLevel level)
        {
            BaseLevel = level;
            cols = level.Cols;
            rows = level.Rows;
        }

        public void GenerateLevel()
        {
            // foreach (var blockProperties in BaseLevel.LevelData)
            // {
            //     var tempBlock = new BlockBuilder();
            //     var t = tempBlock.Build(blockProperties.Block);
            //
            //     t.transform.parent = BlockParent;
            //
            //     t.transform.position = new Vector3(blockProperties.Y * CellSize, blockProperties.X * CellSize);
            // }
        }
    }
}
using Code.DataConfig.BaseObjects;
using UnityEngine;
namespace Code
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private BaseLevel baseLevel;
        [SerializeField] private int cellSize = 50;
        [SerializeField] private Transform blockParent;

        private int _cols;
        private int _rows;
        private int[,] _grid;

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
            // _cols = level.cols;
            // _rows = level.rows;
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
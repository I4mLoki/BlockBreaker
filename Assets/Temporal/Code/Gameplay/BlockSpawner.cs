using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField]
        private BlockBehaviour blockPrefab;

        [SerializeField]
        private int playWidth = 8;

        [SerializeField]
        private float distanceBetweenBlocks = 0.7f;

        private int rowsSpawned;

        private List<BlockBehaviour> blocksSpawned;

        private void Awake()
        {
            blocksSpawned = new List<BlockBehaviour>();
            SpawnRowOfBlocks();
        }

        public void SpawnRowOfBlocks()
        {
            foreach (var block in blocksSpawned.Where(block => block != null))
                block.transform.position += Vector3.down * distanceBetweenBlocks;

            for (var i = 0; i < playWidth; i++)
            {
                if (Random.Range(0, 100) > 30) continue;
                var block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                var hits = Random.Range(1, 3) + rowsSpawned;

                block.SetHits(hits);

                blocksSpawned.Add(block);
            }

            rowsSpawned++;
        }

        private Vector3 GetPosition(int i)
        {
            var position = transform.position;
            position += Vector3.right * i * distanceBetweenBlocks;
            return position;
        }
    }
}
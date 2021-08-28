using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private BlockBehaviour blockBehaviour;

    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    [SerializeField]
    private GameObject bottomWall;

    [SerializeField]
    private GameObject blockContainer;

    private int numberOfRows = 60;
    private int numberOfColumns = 5;
    private float cellSize;
    private Vector3 initialPosition;

    public List<BlockTest> blockList { get; private set; }
    public static GameplayManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        blockContainer.transform.position = new Vector3(leftWall.transform.position.x, bottomWall.transform.position.y);
        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        initialPosition = blockContainer.transform.position;
        cellSize = distance / numberOfColumns;
        InstantiateGrid();
    }

    private void InstantiateGrid()
    {
        blockList = SampleBlockList();

        for (var column = 0; column < numberOfRows; column++)
        {
            for (var row = 0; row < numberOfColumns; row++)
            {
                var currentBlock = blockList.Find(block => block.Position == new Vector3(row, column));
                if (currentBlock == null) continue;

                Vector3 blockPosition;
                if (column == 0 && row == 0) blockPosition = initialPosition;
                else blockPosition = new Vector3(row * cellSize, column * cellSize) + initialPosition;

                InstantiateBlock(blockPosition, currentBlock);
            }
        }
    }

    private void InstantiateBlock(Vector3 localPosition, BlockTest blockTest)
    {
        var newBlock = Instantiate(blockBehaviour, localPosition + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity, blockContainer.transform);
        newBlock.transform.localScale = new Vector3(cellSize, cellSize) * 100f;
        newBlock.SetHits(blockTest.Life);
    }

    public void EnemiesTurn()
    {
        foreach (var block in blockList)
        {
            
        }
        blockContainer.transform.DOMoveY(blockContainer.transform.position.y - cellSize, 1f);
    }

    private List<BlockTest> SampleBlockList()
    {
        var list = new List<BlockTest>();

        var item1 = new BlockTest
        {
            Life = 1,
            Position = new Vector3(0, 0),
            Movement = new Vector2(0, -1)
        };

        var item2 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(1, 4),
            Movement = new Vector2(0, -1)
        };
        
        var item3 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(2, 4),
            Movement = new Vector2(0, -1)
        };
        
        var item4 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(0, 5),
            Movement = new Vector2(0, -1)
        };
        
        var item5 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(1, 5),
            Movement = new Vector2(0, -1)
        };
        
        var item6 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(4, 5),
            Movement = new Vector2(0, -1)
        };
        
        var item7 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(0, 10),
            Movement = new Vector2(0, -1)
        };

        list.Add(item1);
        list.Add(item2);
        list.Add(item3);
        list.Add(item4);
        list.Add(item5);
        list.Add(item6);
        list.Add(item7);

        return list;
    }
}

public class BlockTest
{
    public int Life;
    public Vector3 Position;
    public Vector2 Movement;
}
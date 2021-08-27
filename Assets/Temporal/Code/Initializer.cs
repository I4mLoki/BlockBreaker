using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class Initializer : MonoBehaviour
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
    private GameObject gridParent;

    private int numberOfRows = 60;
    private int numberOfColumns = 5;
    private float cellSize;
    private Vector3 initialPosition;

    private void Start()
    {
        gridParent.transform.position = new Vector3(leftWall.transform.position.x, bottomWall.transform.position.y);
        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        initialPosition = gridParent.transform.position;
        cellSize = distance / numberOfColumns;
        InstantiateGrid();
    }

    private void InstantiateGrid()
    {
        var brickList = SampleBlockList();

        for (var column = 0; column < numberOfRows; column++)
        {
            for (var row = 0; row < numberOfColumns; row++)
            {
                var currentBlock = brickList.Find(block => block.Position == new Vector3(row, column));
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
        var newBlock = Instantiate(blockBehaviour, localPosition + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity, gridParent.transform);
        newBlock.transform.localScale = new Vector3(cellSize, cellSize) * 100f;
        newBlock.SetHits(blockTest.Life);
    }

    private List<BlockTest> SampleBlockList()
    {
        var list = new List<BlockTest>();

        var item1 = new BlockTest
        {
            Life = 1,
            Position = new Vector3(0, 4)
        };

        var item2 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(2, 5)
        };
        
        var item3 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(2, 4)
        };
        
        var item4 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(0, 5)
        };
        
        var item5 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(4, 1)
        };
        
        var item6 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(4, 30)
        };
        
        var item7 = new BlockTest
        {
            Life = 2,
            Position = new Vector3(0, 0)
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
}
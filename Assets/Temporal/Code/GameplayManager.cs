using System;
using System.Collections.Generic;
using DataConfig;
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

    [SerializeField]
    private BaseLevelList levelList;

    private float cellSize;
    private Vector3 initialPosition;

    /*
     * Current level properties
     */
    private BaseLevel level;
    private List<BaseBlockProperties> blockList;

    public static GameplayManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var desiredLevel = 1;
        level = levelList.LevelList.Find(lvl => lvl.LevelNumber == desiredLevel);

        if (level == null)
        {
            Debug.LogError($"Unable to find level {desiredLevel} in BaseLevelList, gameplay aborted.");
            return;
        }

        blockContainer.transform.position = new Vector3(leftWall.transform.position.x, bottomWall.transform.position.y);
        initialPosition = blockContainer.transform.position;
        blockList = level.LevelData;

        GridSetup();
    }

    private void GridSetup()
    {
        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);

        cellSize = distance / levelList.LevelList[0].Cols;
        InstantiateGrid();
    }

    private void InstantiateGrid()
    {
        for (var column = 0; column < levelList.LevelList[0].Cols; column++)
        {
            for (var row = 0; row < levelList.LevelList[0].Rows; row++)
            {
                var currentBlock = blockList.Find(block => new Vector3(block.X, block.Y) == new Vector3(column, row));
                if (currentBlock == null) continue;

                Vector3 blockPosition;
                if (column == 0 && row == 0) blockPosition = initialPosition;
                else blockPosition = new Vector3(row * cellSize, column * cellSize) + initialPosition;

                InstantiateBlock(blockPosition, currentBlock);
            }
        }
    }

    private void InstantiateBlock(Vector3 localPosition, BaseBlockProperties blockTest)
    {
        var newBlock = Instantiate(blockBehaviour, localPosition + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity, blockContainer.transform);
        newBlock.transform.localScale = new Vector3(cellSize, cellSize) * 100f;
        newBlock.SetHits(blockTest.Hits);
    }

    public void EnemiesTurn()
    {
        blockContainer.transform.DOMoveY(blockContainer.transform.position.y - cellSize, 1f);
    }
}
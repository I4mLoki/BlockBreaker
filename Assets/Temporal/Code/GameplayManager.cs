using System;
using System.Collections.Generic;
using DataConfig;
using DataConfig.Tools;
using DG.Tweening;
using Gameplay;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
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

    public List<Block> blockList { get; private set; }
    public static GameplayManager Instance { get; private set; }

    public bool CanPlay { get; private set; }

    private void Awake()
    {
        Instance = this;
        blockList = new List<Block>();
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

        GridSetup();
    }

    private void GridSetup()
    {
        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);

        cellSize = distance / level.Cols;
        InstantiateGrid();
    }

    private void InstantiateGrid()
    {
        var list = level.LevelData;
            
        for (var row = 0; row < level.Rows; row++)
        {
            for (var column = 0; column < level.Cols; column++)
            {
                var currentBlock = list.Find(block => new Vector3(block.X, block.Y) == new Vector3(row, column));
                if (currentBlock == null) continue;

                Vector3 blockPosition;
                if (column == 0 && row == 0) blockPosition = initialPosition;
                else blockPosition = new Vector3(column * cellSize, row * cellSize) + initialPosition;

                InstantiateBlock(blockPosition, currentBlock);
            }
        }

        CanPlay = true;
    }

    private void InstantiateBlock(Vector3 localPosition, BaseBlockProperties blockProperties)
    {
        var block = BlockBuilder.Build(blockProperties, localPosition, blockContainer, cellSize);
        blockList.Add(block);
    }

    public void EnemiesTurn()
    {
        var defaultBlockMovement = new Vector3(0, -1) * cellSize;

        for (var i = 0; i < blockList.Count; i++)
        {
            var newPosition = new Vector3(blockList[i].transform.position.x, blockList[i].transform.position.y) + defaultBlockMovement;
            var isLastBlock = i == blockList.Count - 1;

            blockList[i].transform.DOMove(newPosition, 1f);

            CanPlay = isLastBlock;
        }
    }
}
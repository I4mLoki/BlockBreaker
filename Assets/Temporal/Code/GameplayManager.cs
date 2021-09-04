using System;
using System.Collections;
using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using Code.DataConfig.DataLists;
using Gameplay;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private BaseLevelList levelList;

    private int maxActiveBlocksAllowed = 50;
    private bool levelLoaded;

    /*
     * Current level properties
     */
    private BaseLevel level;

    public List<Block> blockList { get; private set; }
    public static GameplayManager Instance { get; private set; }

    public bool CanPlay { get; private set; }

    private GameplayGridSetup gridSetup;

    private void Awake()
    {
        Instance = this;
        blockList = new List<Block>();

        gridSetup = GetComponent<GameplayGridSetup>();
    }

    private void Start()
    {
        var desiredLevel = 1;
        level = levelList.List.Find(lvl => lvl.levelNumber == desiredLevel);

        if (level == null)
        {
            Debug.LogError($"Unable to find level {desiredLevel} in BaseLevelList, gameplay aborted.");
            return;
        }

        StartCoroutine(gridSetup.InitialLoad(level));
    }

    private void Update()
    {
        // if (levelLoaded || level.LevelData.IsNullOrEmpty()) return;
        // print(blockList.Count);
        //
        // float progress = (blockList.Count * 100) / 3500;
        // GameManager.Instance.SetProgress(progress);
        //
        // if (blockList.Count == 3500)
        // {
        //     levelLoaded = true;
        // }
    }

    // private void GridSetup()
    // {
    //     var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
    //
    //     cellSize = distance / level.Cols;
    //     StartCoroutine(InstantiateGrid());
    // }

    // private IEnumerator InstantiateGrid()
    // {
    //     CanPlay = true;
    //     var list = level.LevelData;
    //     var rowIndexCount = 0;
    //     var rowLimit = level.Rows / 10;
    //
    //     var currentBlock = list[0];
    //
    //     for (var row = 0; row < 500; row++)
    //     {
    //         // if (row < 4) continue;
    //
    //         // yield return new WaitUntil(() => mustLoadMoreBlocks);
    //         
    //         if (rowIndexCount == rowLimit)
    //         {
    //             rowIndexCount = 0;
    //             yield return new WaitForSeconds(.5f);
    //         }
    //         
    //         rowIndexCount++;
    //         
    //         for (var column = 0; column < level.Cols; column++)
    //         {
    //             // var currentBlock = list.Find(block => new Vector3(block.X, block.Y) == new Vector3(row, column));
    //             // if (currentBlock == null) continue;
    //
    //             Vector3 blockPosition;
    //             if (column == 0 && row == 0) blockPosition = initialPosition;
    //             else blockPosition = new Vector3(column * cellSize, row * cellSize) + initialPosition;
    //
    //             InstantiateBlock(blockPosition, currentBlock);
    //         }
    //     }
    // }

    // private IEnumerator Hola()
    // {
    // }

    private void InstantiateBlock(Vector3 localPosition, BaseBlockProperties blockProperties)
    {
        // var block = BlockBuilder.Build(blockProperties, localPosition, blockContainer, cellSize);
        // blockList.Add(block);
    }

    public void EnemiesTurn()
    {
        // var defaultBlockMovement = new Vector3(0, -1) * cellSize;
        //
        // for (var i = 0; i < blockList.Count; i++)
        // {
        //     var newPosition = new Vector3(blockList[i].transform.position.x, blockList[i].transform.position.y) + defaultBlockMovement;
        //     var isLastBlock = i == blockList.Count - 1;
        //
        //     blockList[i].transform.DOMove(newPosition, 1f);
        //
        //     CanPlay = isLastBlock;
        // }
    }
}
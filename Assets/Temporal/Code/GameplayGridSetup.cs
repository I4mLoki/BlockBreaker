using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataConfig;
using DataConfig.Tools;
using DG.Tweening;
using Gameplay;
using Sirenix.Utilities;
using UnityEngine;

public class GameplayGridSetup : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    [SerializeField]
    private GameObject bottomWall;

    [SerializeField]
    private GameObject topWall;

    [SerializeField]
    private GameObject blockContainer;

    private int visibleRows = 11;
    private float cellSize;
    private Vector3 initialPosition;

    private BaseLevel _level;
    private int currentRow;

    private List<BaseBlockProperties> _levelBlockList;
    private List<Block> _loadedBlockList;

    public void InitialLoad(BaseLevel level)
    {
        _level = level;
        // _levelBlockList = _level.LevelData;
        _levelBlockList = _level.LevelData.OrderBy(block => block.X).ThenBy(block => block.Y).ToList();
        _loadedBlockList = new List<Block>();

        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        cellSize = distance / level.Cols;

        topWall.transform.position = new Vector3(topWall.transform.position.x, bottomWall.transform.position.y + visibleRows * cellSize);
        blockContainer.transform.position = new Vector3(leftWall.transform.position.x, topWall.transform.position.y);
        initialPosition = blockContainer.transform.position;

        LoadNextRow();
    }

    public void LoadNextRow()
    {
        var blockListOnThisRow = _levelBlockList.FindAll(block => block.X == currentRow);

        if (blockListOnThisRow.IsNullOrEmpty())
        {
            currentRow++;
            StartCoroutine(MoveBlocks());
        }

        foreach (var blockOnThisRow in blockListOnThisRow)
        {
            Vector3 blockPosition;
            if (blockOnThisRow.Y == 0) blockPosition = initialPosition;
            else blockPosition = new Vector3(blockOnThisRow.Y * cellSize, 0f) + initialPosition;

            var block = BlockBuilder.Build(blockOnThisRow, blockPosition, blockContainer, cellSize);
            _loadedBlockList.Add(block);
        }

        currentRow++;
        StartCoroutine(MoveBlocks());
    }

    private IEnumerator MoveBlocks()
    {
        var defaultBlockMovement = new Vector3(0, -1) * cellSize;

        for (var i = 0; i < _loadedBlockList.Count; i++)
        {
            var newPosition = new Vector3(_loadedBlockList[i].transform.position.x, _loadedBlockList[i].transform.position.y) + defaultBlockMovement;
            var isLastBlock = i == _loadedBlockList.Count - 1;

            _loadedBlockList[i].transform.DOMove(newPosition, 1f);
        }

        yield return new WaitForSeconds(2f);
        LoadNextRow();
    }
}
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

    private int safeArea = 4;

    public IEnumerator InitialLoad(BaseLevel level)
    {
        _level = level;
        _levelBlockList = _level.LevelData;
        _loadedBlockList = new List<Block>();

        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        cellSize = distance / level.Cols;

        topWall.transform.position = new Vector3(topWall.transform.position.x, bottomWall.transform.position.y + visibleRows * cellSize);
        blockContainer.transform.position = new Vector3(leftWall.transform.position.x, topWall.transform.position.y + cellSize);
        initialPosition = blockContainer.transform.position;

        LoadNextRow();
        MoveBlocks();
        
        for (var i = 0; i < visibleRows - safeArea; i++)
        {
            LoadNextRow();
            yield return new WaitForSeconds(.5f);
            MoveBlocks();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadNextRow();
            MoveBlocks();
        }
    }
    
    // private IEnumerator Move

    public void LoadNextRow()
    {
        var blockListOnThisRow = _levelBlockList.FindAll(block => block.X == currentRow);

        if (blockListOnThisRow.IsNullOrEmpty())
        {
            currentRow++;
            return;
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
    }

    private void MoveBlocks()
    {
        var defaultBlockMovement = new Vector3(0, -1) * cellSize;

        foreach (var block in _loadedBlockList)
        {
            var newPosition = new Vector3(block.transform.position.x, block.transform.position.y) + defaultBlockMovement;
            block.transform.DOMove(newPosition, .5f);
        }
    }
}
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

    private int _visibleRows = 11;
    private float _cellSize;
    private Vector3 _initialPosition;

    private BaseLevel _level;
    private int _currentRow;

    private List<BaseBlockProperties> _levelBlockList;
    private List<Block> _loadedBlockList;

    private int _safeArea = 4;

    public IEnumerator InitialLoad(BaseLevel level)
    {
        _level = level;
        _levelBlockList = _level.LevelData;
        _loadedBlockList = new List<Block>();

        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        _cellSize = distance / level.Cols;

        topWall.transform.position = new Vector3(topWall.transform.position.x, bottomWall.transform.position.y + _visibleRows * _cellSize);
        blockContainer.transform.position = new Vector3(leftWall.transform.position.x, topWall.transform.position.y + _cellSize);
        _initialPosition = blockContainer.transform.position;

        LoadNextRow();
        MoveBlocks();
        
        for (var i = 0; i < _visibleRows - _safeArea; i++)
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
        var blockListOnThisRow = _levelBlockList.FindAll(block => block.X == _currentRow);

        if (blockListOnThisRow.IsNullOrEmpty())
        {
            _currentRow++;
            return;
        }

        foreach (var blockOnThisRow in blockListOnThisRow)
        {
            Vector3 blockPosition;
            if (blockOnThisRow.Y == 0) blockPosition = _initialPosition;
            else blockPosition = new Vector3(blockOnThisRow.Y * _cellSize, 0f) + _initialPosition;

            var block = BlockBuilder.Build(blockOnThisRow, blockPosition, blockContainer, _cellSize);
            _loadedBlockList.Add(block);
        }

        _currentRow++;
    }

    private void MoveBlocks()
    {
        var defaultBlockMovement = new Vector3(0, -1) * _cellSize;

        foreach (var block in _loadedBlockList)
        {
            var newPosition = new Vector3(block.transform.position.x, block.transform.position.y) + defaultBlockMovement;
            block.transform.DOMove(newPosition, .5f);
        }
    }
}
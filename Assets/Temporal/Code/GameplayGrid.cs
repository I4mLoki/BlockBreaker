using System;
using System.Collections;
using System.Collections.Generic;
using Code.DataConfig.BaseObjects;
using Code.DataConfig.Tools;
using DataConfig.Tools;
using DG.Tweening;
using Gameplay;
using Sirenix.Utilities;
using UnityEngine;

public class GameplayGrid : MonoBehaviour
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

    public void Setup(BaseLevel level, Action<float, int, int> callback)
    {
        var distance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        var cellSize = distance/level.cols;
        var visibleColumns = level.cols;
        var visibleRows = 11;

        topWall.transform.position = new Vector3(topWall.transform.position.x, bottomWall.transform.position.y + visibleRows*cellSize);
        blockContainer.transform.position = new Vector3(leftWall.transform.position.x, topWall.transform.position.y + cellSize);
        
        callback.Invoke(cellSize, visibleColumns, visibleRows);
    }
}
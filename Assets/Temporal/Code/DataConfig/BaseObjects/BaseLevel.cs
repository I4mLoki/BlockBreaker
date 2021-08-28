using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataConfig
{
    [Serializable]
    public class BaseLevel : ScriptableObject
    {
        [Header("Level Config")]
        [SerializeField] private int levelNumber = 0;
        [SerializeField] private  int rows;
        [SerializeField] private  int _cols;
        [SerializeField] private int safeArea;
        [SerializeField] private  int blocks;
        [Header("Score")]
        [SerializeField] private  int star1Score;
        [SerializeField] private  int star2Score;
        [SerializeField] private  int star3Score;
        [Header("Textures")]
        [SerializeField] private  Texture2D background;
        [Header("Level Data")]
        [SerializeField] private  List<BaseBlockProperties> levelData;

        public int LevelNumber
        {
            get => levelNumber;
            set => levelNumber = value;
        }

        public int Rows
        {
            get => rows;
            set => rows = value;
        }

        public int Cols
        {
            get => _cols;
            set => _cols = value;
        }

        public int Blocks
        {
            get => blocks;
            set => blocks = value;
        }

        public int Star1Score
        {
            get => star1Score;
            set => star1Score = value;
        }

        public int Star2Score
        {
            get => star2Score;
            set => star2Score = value;
        }

        public int Star3Score
        {
            get => star3Score;
            set => star3Score = value;
        }

        public Texture2D Background
        {
            get => background;
            set => background = value;
        }

        public List<BaseBlockProperties> LevelData
        {
            get => levelData;
            set => levelData = value;
        }

        public int SafeArea
        {
            get => safeArea;
            set => safeArea = value;
        }
    }
}
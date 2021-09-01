using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Demos.RPGEditor;
using UnityEditor;
using UnityEngine;

namespace DataConfig
{
    [InlineEditor]
    public class BaseLevel : SerializedScriptableObject
    {
        [BoxGroup("Level Config"), ColorBox]
        public int levelNumber = 0;

        [BoxGroup("Level Config"), ColorBox]
        [ShowInInspector, OnValueChanged("Resize")]
        public int rows = 10;

        [BoxGroup("Level Config"), ColorBox]
        [ShowInInspector, OnValueChanged("Resize")]
        public int cols = 7;

        [BoxGroup("Level Config"), ColorBox]
        public int safeArea = 0;

        [BoxGroup("Level Config"), ColorBox]
        public int blocks = 0;

        [BoxGroup("Score"), ColorBox]
        public int star1Score = 300;

        [BoxGroup("Score"), ColorBox]
        public int star2Score = 600;

        [BoxGroup("Score"), ColorBox]
        public int star3Score = 900;

        [BoxGroup("Textures"), ColorBox]
        public Texture2D background = null;

        [BoxGroup("Level Data"), ColorBox]
        public List<BaseBlock> levelData;

        [BoxGroup("Level Data"), HideLabel, ColorBox, OnValueChanged("SetToList")]
        public BaseBlock[,] dataTable;
        private BaseBlock[,] _tempTable = new BaseBlock[0,0];

        public void Resize()
        {
            dataTable = new BaseBlock[cols, rows];
        }

        private IEnumerable<BaseLevel> GetBaseLevelList()
        {
            var level = AssetDatabase.FindAssets("t:ScriptableObject")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<BaseLevel>);

            return level;
        }

        public void SetToList()
        {
            levelData.Clear();
            var col = dataTable.GetLength(0);
            var row = dataTable.GetLength(1);

            for (var i = 0; i < col; i++)
            {
                for (var j = 0; j < row; j++)
                {
                    var t = dataTable[i, j];
                    if (t == null)
                        continue;
                    t.blockProperties.x = j;
                    t.blockProperties.y = i;
                    t.blockProperties.hits = dataTable[i, j].blockProperties.hits;
                    levelData.Add(t);
                }
            }
        }
    }
}
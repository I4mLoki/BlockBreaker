using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Code.DataConfig.BaseObjects
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
        public List<BaseBlockProperties> levelData;

        [BoxGroup("Level Data"), HideLabel, ColorBox, OnValueChanged("SetToList")]
        public BaseBlockProperties[,] dataTable = new BaseBlockProperties[7, 10];

        public void Resize()
        {
            levelData.Clear();
            dataTable = new BaseBlockProperties[cols, rows];
        }

        public void SetToList()
        {
            levelData.Clear();
            var tempTable = dataTable;
            dataTable = new BaseBlockProperties[cols, rows];

            var col = dataTable.GetLength(0);
            var row = dataTable.GetLength(1);

            for (var i = 0; i < col; i++)
            {
                for (var j = 0; j < row; j++)
                {
                    var data = tempTable[i, j];
                    if (data.block == null)
                    {
                        continue;
                    }
                    data.block = tempTable[i, j].block;
                    ;
                    data.hits = tempTable[i, j].hits;
                    data.x = j;
                    data.y = i;
                    levelData.Add(data);
                }
            }
            dataTable = tempTable;
        }

        private IEnumerable<BaseBlock> GetListOfBaseBlocks()
        {
            return Object.FindObjectsOfType<BaseBlock>();
        }
    }
}
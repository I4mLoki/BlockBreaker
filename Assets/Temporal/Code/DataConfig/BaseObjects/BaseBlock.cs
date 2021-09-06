using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
namespace Code.DataConfig.BaseObjects
{
    public class BaseBlock : SerializedScriptableObject
    {
        [BoxGroup("Basic Info"), ColorBox]
        public string blockName = "New Block ";

        [BoxGroup("Basic Info"), ColorBox]
        public GameObject blockPrefab = null;

        [BoxGroup("Game Data")]
        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockHeads = null;

        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockEyes = null;

        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockEars = null;

        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockMouth = null;

        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockHips = null;

        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockArms = null;

        [BoxGroup("Game Data/Textures"), ColorBox]
        public List<Texture2D> blockLegs = null;

        [BoxGroup("Game Data/Materials"), ColorBox]
        public List<Material> blockMaterials = null;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public BaseBlockProperties blockProperties;

        [BoxGroup("Basic Info"), ColorBox]
        public Texture2D blockIcon = null;

        [BoxGroup("Game Data/Properties"), ColorBox, Space]
        public int sizeX = 1;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public int sizeY = 1;

        [BoxGroup("Game Data/Movement Paths")]
        public List<Vector2> movementTiles;

        [OnValueChanged("SetToList"), FoldoutGroup("Movement"),
         TableMatrix(HorizontalTitle = "Custom Movement Drawing",
             DrawElementMethod = "DrawElements",
             SquareCells = true,
             ResizableColumns = false,
             RowHeight = 5,
             HideColumnIndices = true,
             HideRowIndices = true)]
        public bool[,] CustomCellDrawing = new bool[5, 5];

        [BoxGroup("Game Data/Properties"), ColorBox, Space]
        public bool shouldBeUnique = false;


        private void SetToList()
        {
            var y = CustomCellDrawing.GetLength(0);
            var x = CustomCellDrawing.GetLength(1);
            var tempTable = CustomCellDrawing;
            var tempValue = new Vector2();

            movementTiles = new List<Vector2>();

            for (var i = 0; i < y; i++)
            {
                for (var j = 0; j < x; j++)
                {
                    var data = tempTable[i, j];
                    if (i == 2 && j == 2)
                    {
                        data = true;
                        continue;
                    }
                    if (data == true)
                    {
                        tempValue.x = i - 2;
                        tempValue.y = j - 2;
                        movementTiles.Add(tempValue);
                    }
                    else
                    {
                        tempTable[i, j] = new bool();
                        continue;
                    }
                }
            }
            CustomCellDrawing = tempTable;
        }

        private static bool DrawElements(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));

            return value;
        }

        [OnInspectorInit]
        private void CreateData()
        {
            this.CustomCellDrawing = new bool[5, 5];
            this.CustomCellDrawing[2, 2] = true;
        }
    }
}
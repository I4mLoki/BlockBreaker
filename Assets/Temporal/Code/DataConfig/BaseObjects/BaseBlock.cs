using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Code.DataConfig.DataLists;
using Code.DataConfig.Tools.Inspector.Attributes;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace Code.DataConfig.BaseObjects
{
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public class BaseBlock : SerializedScriptableObject
    {
        [FoldoutGroup("Basic Info")]
        [BoxGroup("Basic Info/Info"), ColorBox]
        public string blockName = "New Block ";

        [BoxGroup("Basic Info/Info"), ColorBox]
        public GameObject blockPrefab = null;

        [FoldoutGroup("Game Data")]
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

        [BoxGroup("Basic Info/Info"), ColorBox]
        public Texture2D blockIcon = null;

        [BoxGroup("Game Data/Properties"), ColorBox, Space]
        public int sizeX = 1;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public int sizeY = 1;

        [FoldoutGroup("Animations")]
        [ColorBox]
        public List<AnimationClip> animationList;



        [FoldoutGroup("Behaviour")]
        [BoxGroup("Behaviour/Template"), ColorBox, ActionButton("AddBehaviourToList")]
        public AnimatorController behaviourData;

        [BoxGroup("Behaviour/Template"), HorizontalGroup("Behaviour/Template/Temp"), ColorBox, HideLabel, ValueDropdown("TargetsValues")]
        public string target = "";

        [BoxGroup("Behaviour/Template"), HorizontalGroup("Behaviour/Template/Temp"), ColorBox, HideLabel, ValueDropdown("FieldsValues")]
        public string field = "";

        [BoxGroup("Behaviour/Template"), HorizontalGroup("Behaviour/Template/Temp"), ColorBox, HideLabel]
        public AnimatorConditionMode condition = AnimatorConditionMode.Equals;

        [BoxGroup("Behaviour/Template"), HorizontalGroup("Behaviour/Template/Temp"), ColorBox, HideLabel]
        public int value = 0;

        [BoxGroup("Behaviour/Template"), HorizontalGroup("Behaviour/Template/Temp"), ColorBox, HideLabel, ValueDropdown("BbParam")]
        public string parameter = "";

        [BoxGroup("Behaviour/Behaviour List"), ColorBox, TableList(HideToolbar = true), Space]
        public List<BlockBehaviour> behaviourTable = new List<BlockBehaviour>();



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



        private List<string> BbParam { get; set; }
        private List<string> BbStates { get; set; }

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
            CustomCellDrawing[2, 2] = true;
        }

        private void AddBehaviourToList()
        {
            var item = BlockBehaviour.AddBehaviourToList(target, field, condition, value, parameter, this);

            behaviourTable.Add(item);
            ResetBehaviourValues();
        }

        private void ResetBehaviourValues()
        {
            target = "";
            field = "";
            condition = AnimatorConditionMode.Equals;
            value = 0;
            parameter = "";
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

            if (behaviourData == null)
                return;

            BbParam = new List<string>();
            BbStates = new List<string>();

            for (var i = 0; i < behaviourData.parameters.Length; i++)
            {
                BbParam.Add(behaviourData.parameters[i].name);
            }
        }

        private IEnumerable TargetsValues()
        {
           var dataPath = CreateInstance<DataPath>();
           var dataList = AssetDatabase.LoadAssetAtPath<DataList>(dataPath.dataListPath);

           return dataList.variableData.targets;
        }

        private IEnumerable FieldsValues()
        {
            var dataPath = CreateInstance<DataPath>();
            var dataList = AssetDatabase.LoadAssetAtPath<DataList>(dataPath.dataListPath);

            return dataList.variableData.fields;
        }
    }
}
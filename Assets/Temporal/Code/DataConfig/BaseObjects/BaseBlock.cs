using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
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

        [BoxGroup("Game Data/Physics Materials"), ColorBox]
        public PhysicsMaterial2D physicsMaterial = null;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public BaseBlockProperties blockProperties;

        [BoxGroup("Basic Info"), ColorBox]
        public Texture2D blockIcon = null;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public int sizeX = 1;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public int sizeY = 1;

        [BoxGroup("Game Data/Properties"), ColorBox]
        public bool shouldBeUnique = false;

        private IEnumerable<BaseBlock> GetBaseBlockList()
        {
            var block = AssetDatabase.FindAssets("t:ScriptableObject")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<BaseBlock>);

            return block;
        }
    }
}
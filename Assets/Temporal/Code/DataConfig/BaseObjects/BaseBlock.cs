using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataConfig
{
    [InlineEditor]
    public class BaseBlock : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        public string blockName = "New Block ";
        [BoxGroup("Basic Info")]
        public GameObject blockPrefab = null;

        public List<Texture2D> blockHeads = null;
        public List<Texture2D> blockEyes = null;
        public List<Texture2D> blockEars = null;
        public List<Texture2D> blockMouth = null;
        public List<Texture2D> blockHips = null;
        public List<Texture2D> blockArms = null;
        public List<Texture2D> blockLegs = null;

        public List<Material> blockMaterials = null;

        public Texture2D blockIcon = null;
        public int sizeX = 1;
        public int sizeY = 1;

        public bool shouldBeUnique = false;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataConfig
{
    [Serializable]
    public class BaseBlock : ScriptableObject
    {
        [SerializeField] private string blockName = "New Block ";
        [SerializeField] private GameObject blockPrefab = null;

        [SerializeField] private List<Texture2D> blockHeads = null;
        [SerializeField] private List<Texture2D> blockEyes = null;
        [SerializeField] private List<Texture2D> blockEars = null;
        [SerializeField] private List<Texture2D> blockMouth = null;
        [SerializeField] private List<Texture2D> blockHips = null;
        [SerializeField] private List<Texture2D> blockArms = null;
        [SerializeField] private List<Texture2D> blockLegs = null;

        [SerializeField] private List<Material> blockMaterials = null;

        [SerializeField] private Texture2D blockIcon = null;
        [SerializeField] private int sizeX = 1;
        [SerializeField] private int sizeY = 1;

        [SerializeField] private bool shouldBeUnique = false;


        public string BlockName
        {
            get => blockName;
            set => blockName = value;
        }

        public GameObject BlockPrefab
        {
            get => blockPrefab;
            set => blockPrefab = value;
        }

        public List<Texture2D> BlockHeads
        {
            get => blockHeads;
            set => blockHeads = value;
        }

        public List<Texture2D> BlockEyes
        {
            get => blockEyes;
            set => blockEyes = value;
        }

        public List<Texture2D> BlockEars
        {
            get => blockEars;
            set => blockEars = value;
        }

        public List<Texture2D> BlockMouth
        {
            get => blockMouth;
            set => blockMouth = value;
        }

        public List<Texture2D> BlockHips
        {
            get => blockHips;
            set => blockHips = value;
        }

        public List<Texture2D> BlockArms
        {
            get => blockArms;
            set => blockArms = value;
        }

        public List<Texture2D> BlockLegs
        {
            get => blockLegs;
            set => blockLegs = value;
        }

        public List<Material> BlockMaterials
        {
            get => blockMaterials;
            set => blockMaterials = value;
        }

        public Texture2D BlockIcon
        {
            get => blockIcon;
            set => blockIcon = value;
        }

        public int SizeX
        {
            get => sizeX;
            set => sizeX = value;
        }

        public int SizeY
        {
            get => sizeY;
            set => sizeY = value;
        }

        public bool ShouldBeUnique
        {
            get => shouldBeUnique;
            set => shouldBeUnique = value;
        }
    }
}
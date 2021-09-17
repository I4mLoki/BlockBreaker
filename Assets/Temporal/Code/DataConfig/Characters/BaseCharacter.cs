using Sirenix.OdinInspector;
using UnityEngine;
namespace Code.DataConfig.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "BlockBreaker/Data/Character", order = 0)]
    [InlineEditor]
    public class BaseCharacter : SerializedScriptableObject
    {
        [FoldoutGroup("Info")]
        [BoxGroup("Info/Data"), ColorBox]
        public string name = "New Character ";

        [FoldoutGroup("Info")]
        [BoxGroup("Info/Data"), ColorBox, TextArea(5, 8)]
        public string description =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque at purus nulla. Vivamus urna felis, egestas vitae felis vitae, condimentum mattis arcu. Proin consectetur justo ac metus mattis vestibulum. Suspendisse ac euismod arcu, scelerisque ultricies ante. Aenean est arcu, efficitur consectetur risus eu, iaculis fermentum velit. Sed vitae turpis consectetur, condimentum mauris nec, eleifend turpis. Suspendisse condimentum pretium eros, in mattis eros faucibus ac. Nulla et sollicitudin felis. Quisque non lacus in dolor consequat pellentesque. Vestibulum eleifend augue vel ipsum condimentum, in tristique odio convallis.";

        [FoldoutGroup("Info")]
        [BoxGroup("Info/Textures"), ColorBox]
        [OnInspectorGUI("DrawPreview", append: true)]
        public Texture2D portrait;

        [FoldoutGroup("Info")]
        [BoxGroup("Info/Textures"), ColorBox]
        public Texture2D icon;

        [FoldoutGroup("Info")]
        [BoxGroup("Info/Data"), ColorBox]
        [OnInspectorGUI("DrawPreview", append: true)]
        public bool locked;

        [FoldoutGroup("Info")]
        [BoxGroup("Info/Data"), ColorBox]
        public GameObject prefab;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Stats"), ColorBox, PropertyRange(0, 9999)]
        public int power = 10;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Stats"), ColorBox, Range(0, 9999)]
        public int health = 20;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Stats"), ColorBox, Range(0, 9999), InfoBox("Damage per ball", InfoMessageType.None)]
        public float damage = 5.0f;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Stats"), ColorBox, Range(0, 100)]
        public int balls = 5;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Chances"), ColorBox, Range(0, 100)]
        public float blockPossibility = 0.0f;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Chances"), ColorBox, Range(0, 100)]
        public float blockQuantity = 0.0f;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Chances"), ColorBox, Range(0, 100)]
        public float criticalChance = 3.0f;

        [FoldoutGroup("Attributes")]
        [BoxGroup("Attributes/Chances"), ColorBox, Range(0, 100)]
        public float criticalMultiplier = 2.0f;


        private void DrawPreview()
        {
            if (this.portrait == null) return;

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(this.portrait);
            GUILayout.EndVertical();
        }

    }
}
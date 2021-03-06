using Code.DataConfig.BaseObjects;
using Code.DataConfig.DataLists;
using Gameplay;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;
namespace Code.Editor
{
    public class CreateBlockData
    {
        private DataList _dataList;

        [InlineEditor(Expanded = true)]
        public BaseBlock blockData;
        public CreateBlockData(DataList dataList)
        {
            blockData = ScriptableObject.CreateInstance<BaseBlock>();
            blockData.blockName = "Block " + (dataList.baseBlockList.List.Count + 1);
            _dataList = dataList;
        }

        [Button("Add New Block")]
        private void CreateNewData()
        {
            var go = new GameObject("New Block");

            AssetDatabase.CreateAsset(blockData,_dataList.dataPath.blocksPath + "/" + blockData.blockName + ".asset");
            AssetDatabase.SaveAssets();
            _dataList.baseBlockList.List.Add(blockData);

            var text = new GameObject("Text");
            text.transform.parent = go.transform;
            text.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            var blockParts = ObjectFactory.AddComponent<BlockParts>(go);
            blockParts.baseBlock = blockData;

            //Add Block Script
            ObjectFactory.AddComponent<Block>(go);

            //Add Block Script
            var textTemp = ObjectFactory.AddComponent<TextMeshPro>(text);
            textTemp.alignment = TextAlignmentOptions.Center;
            textTemp.autoSizeTextContainer = true;
            textTemp.color = Color.white;

            //Add SpriteRendererComponent
            ObjectFactory.AddComponent<SpriteRenderer>(go);

            //Add BoxCollider and PhysicsMaterial
            // ObjectFactory.AddComponent<BoxCollider2D>(go).sharedMaterial =
            //     AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/Temporal/Materials/Bouncy.physicsMaterial2D");

            var art = new GameObject("Art");
            art.transform.parent = go.transform;

            var head = new GameObject("head");
            var leftEye = new GameObject("leftEye");
            var rightEye = new GameObject("rightEye");
            var leftEar = new GameObject("leftEar");
            var rightEar = new GameObject("rightEar");
            var mouth = new GameObject("mouth");
            var hip = new GameObject("hip");
            var leftArm = new GameObject("leftArm");
            var rightArm = new GameObject("rightArm");
            var leftLeg = new GameObject("leftLeg");
            var rightLeg = new GameObject("rightLeg");

            SpriteRenderer headImage = ObjectFactory.AddComponent<SpriteRenderer>(head);
            SpriteRenderer leftEyeImage = ObjectFactory.AddComponent<SpriteRenderer>(leftEye);
            SpriteRenderer rightEyeImage = ObjectFactory.AddComponent<SpriteRenderer>(rightEye);
            SpriteRenderer leftEarImage = ObjectFactory.AddComponent<SpriteRenderer>(leftEar);
            SpriteRenderer rightEarImage = ObjectFactory.AddComponent<SpriteRenderer>(rightEar);
            SpriteRenderer mouthImage = ObjectFactory.AddComponent<SpriteRenderer>(mouth);
            SpriteRenderer hipImage = ObjectFactory.AddComponent<SpriteRenderer>(hip);
            SpriteRenderer leftArmImage = ObjectFactory.AddComponent<SpriteRenderer>(leftArm);
            SpriteRenderer rightArmImage = ObjectFactory.AddComponent<SpriteRenderer>(rightArm);
            SpriteRenderer leftLegImage = ObjectFactory.AddComponent<SpriteRenderer>(leftLeg);
            SpriteRenderer rightLegImage = ObjectFactory.AddComponent<SpriteRenderer>(rightLeg);

            blockParts.head = headImage;
            blockParts.leftEye = leftEyeImage;
            blockParts.rightEye = rightEyeImage;
            blockParts.leftEar = leftEarImage;
            blockParts.rightEar = rightEarImage;
            blockParts.leftArm = leftArmImage;
            blockParts.rightArm = rightArmImage;
            blockParts.leftLeg = leftLegImage;
            blockParts.rightLeg = rightLegImage;
            blockParts.mouth = mouthImage;
            blockParts.hip = hipImage;

            head.transform.parent = art.transform;
            leftEye.transform.parent = art.transform;
            rightEye.transform.parent = art.transform;
            leftEar.transform.parent = art.transform;
            rightEar.transform.parent = art.transform;
            leftArm.transform.parent = art.transform;
            rightArm.transform.parent = art.transform;
            leftLeg.transform.parent = art.transform;
            rightLeg.transform.parent = art.transform;
            mouth.transform.parent = art.transform;
            hip.transform.parent = art.transform;


            var prefab = PrefabUtility.SaveAsPrefabAsset(go,
                _dataList.dataPath.prefabPath + blockData.blockName + ".prefab");

            blockData.blockPrefab = prefab;

            UnityEngine.Object.DestroyImmediate(go);
        }
    }
}
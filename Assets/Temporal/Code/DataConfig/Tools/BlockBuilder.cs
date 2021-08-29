using Gameplay;
using UnityEditor;
using UnityEngine;

namespace DataConfig.Tools
{
    public class BlockBuilder
    {
        public static Block Build(BaseBlockProperties _baseBlockProperties, Vector3 localPosition, GameObject parent, float cellSize)
        {
            var tempBaseBlock = Object.Instantiate(_baseBlockProperties.Block.BlockPrefab, localPosition + new Vector3(cellSize, cellSize) * .5f, Quaternion.identity, parent.transform);
            
            tempBaseBlock.GetComponent<SpriteRenderer>().size = new Vector2(cellSize, cellSize);
            var collider = tempBaseBlock.AddComponent<BoxCollider2D>();
            collider.sharedMaterial = AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/Temporal/Materials/Bouncy.physicsMaterial2D");
            collider.size = new Vector2(cellSize, cellSize);

            var tempBlock = tempBaseBlock.GetComponent<Block>();
            
            tempBlock.SetHits(_baseBlockProperties.Hits);

            // var t = tempBlock.GetComponent<BlockParts>();
            //
            // var head = _baseBlock.BlockHeads[Random.Range(0, _baseBlock.BlockHeads.Count)];
            // var leftEye = _baseBlock.BlockEyes[Random.Range(0, _baseBlock.BlockEyes.Count)];
            // var rightEye = _baseBlock.BlockEyes[Random.Range(0, _baseBlock.BlockEyes.Count)];
            // var leftEar = _baseBlock.BlockEars[Random.Range(0, _baseBlock.BlockEars.Count)];
            // var rightEar = _baseBlock.BlockEars[Random.Range(0, _baseBlock.BlockEars.Count)];
            // var mouth = _baseBlock.BlockMouth[Random.Range(0, _baseBlock.BlockMouth.Count)];
            // var hip = _baseBlock.BlockHips[Random.Range(0, _baseBlock.BlockHips.Count)];
            // var leftArm = _baseBlock.BlockArms[Random.Range(0, _baseBlock.BlockArms.Count)];
            // var rightArm = _baseBlock.BlockArms[Random.Range(0, _baseBlock.BlockArms.Count)];
            // var leftLeg = _baseBlock.BlockLegs[Random.Range(0, _baseBlock.BlockLegs.Count)];
            // var rightLeg = _baseBlock.BlockLegs[Random.Range(0, _baseBlock.BlockLegs.Count)];
            // var material = _baseBlock.BlockMaterials[Random.Range(0, _baseBlock.BlockMaterials.Count)];
            //
            // var mHead =  Sprite.Create(head, new Rect(0.0f, 0.0f, head.width, head.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mleftEye =  Sprite.Create(leftEye, new Rect(0.0f, 0.0f, leftEye.width, leftEye.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mrightEye =  Sprite.Create(rightEye, new Rect(0.0f, 0.0f, rightEye.width, rightEye.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mleftEar =  Sprite.Create(leftEar, new Rect(0.0f, 0.0f, leftEar.width, leftEar.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mrightEar =  Sprite.Create(rightEar, new Rect(0.0f, 0.0f, rightEar.width, rightEar.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mmouth =  Sprite.Create(mouth, new Rect(0.0f, 0.0f, mouth.width, mouth.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mhip =  Sprite.Create(hip, new Rect(0.0f, 0.0f, hip.width, hip.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mleftArm =  Sprite.Create(leftArm, new Rect(0.0f, 0.0f, leftArm.width, leftArm.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mrightArm =  Sprite.Create(rightArm, new Rect(0.0f, 0.0f, rightArm.width, rightArm.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mleftLeg =  Sprite.Create(leftLeg, new Rect(0.0f, 0.0f, leftLeg.width, leftLeg.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mrightLeg =  Sprite.Create(rightLeg, new Rect(0.0f, 0.0f, rightLeg.width, rightLeg.height), new Vector2(0.5f, 0.5f), 100.0f);
            // var mmaterial =  Sprite.Create(rightLeg, new Rect(0.0f, 0.0f, rightLeg.width, rightLeg.height), new Vector2(0.5f, 0.5f), 100.0f);
            //
            // t.SetComponents(mHead, mleftEye, mrightEye, mleftEar, mrightEar, mleftArm, mrightArm, mleftLeg, mrightLeg, mmouth, mhip, material);

            return tempBlock;
        }
    }
}
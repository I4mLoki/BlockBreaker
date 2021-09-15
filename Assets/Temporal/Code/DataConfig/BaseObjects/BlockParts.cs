using UnityEngine;
namespace Code.DataConfig.BaseObjects
{
    public class BlockParts : MonoBehaviour
    {
        public BaseBlock baseBlock;
        public SpriteRenderer head;
        public SpriteRenderer leftEye;
        public SpriteRenderer rightEye;
        public SpriteRenderer leftEar;
        public SpriteRenderer rightEar;
        public SpriteRenderer mouth;
        public SpriteRenderer hip;
        public SpriteRenderer leftArm;
        public SpriteRenderer rightArm;
        public SpriteRenderer leftLeg;
        public SpriteRenderer rightLeg;
        public Material material;

        public void SetComponents(Sprite _head, Sprite _leftEye, Sprite _rightEye, Sprite _leftEar, Sprite _rightEar,
            Sprite _leftArm, Sprite _rightArm, Sprite _leftLeg, Sprite _rightLeg, Sprite _mouth, Sprite _hip,
            Material _material)
        {
            this.head.sprite = _head;
            this.leftEye.sprite = _leftEye;
            this.rightEye.sprite = _rightEye;
            this.leftEar.sprite = _leftEar;
            this.rightEar.sprite = _rightEar;
            this.leftArm.sprite = _leftArm;
            this.rightArm.sprite = _rightArm;
            this.leftLeg.sprite = _leftLeg;
            this.rightLeg.sprite = _rightLeg;
            this.mouth.sprite = _mouth;
            this.hip.sprite = _hip;
            this.material = _material;

            this.head.material = this.material;
            this.leftEye.material = this.material;
            this.rightEye.material = this.material;
            this.leftEar.material = this.material;
            this.rightEar.material = this.material;
            this.leftArm.material = this.material;
            this.rightArm.material = this.material;
            this.leftLeg.material = this.material;
            this.rightLeg.material = this.material;
            this.mouth.material = this.material;
            this.hip.material = this.material;
        }
    }
}
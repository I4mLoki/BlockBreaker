using Temporal.Code.DataConfig.BaseObjects;
using UnityEngine;
using UnityEngine.UI;

namespace DataConfig
{
    public class BlockParts : MonoBehaviour
    {
        [SerializeField] private BaseBlock _baseBlock;
        [SerializeField] private  SpriteRenderer _head;
        [SerializeField] private  SpriteRenderer _leftEye;
        [SerializeField] private  SpriteRenderer _rightEye;
        [SerializeField] private  SpriteRenderer _leftEar;
        [SerializeField] private  SpriteRenderer _rightEar;
        [SerializeField] private  SpriteRenderer _mouth;
        [SerializeField] private  SpriteRenderer _hip;
        [SerializeField] private  SpriteRenderer _leftArm;
        [SerializeField] private  SpriteRenderer _rightArm;
        [SerializeField] private  SpriteRenderer _leftLeg;
        [SerializeField] private  SpriteRenderer _rightLeg;
        [SerializeField] private  Material _material;

        public BaseBlock BaseBlock
        {
            get => _baseBlock;
            set => _baseBlock = value;
        }

        public SpriteRenderer Head
        {
            get => _head;
            set => _head = value;
        }

        public SpriteRenderer LeftEye
        {
            get => _leftEye;
            set => _leftEye = value;
        }

        public SpriteRenderer RightEye
        {
            get => _rightEye;
            set => _rightEye = value;
        }

        public SpriteRenderer LeftEar
        {
            get => _leftEar;
            set => _leftEar = value;
        }

        public SpriteRenderer RightEar
        {
            get => _rightEar;
            set => _rightEar = value;
        }

        public SpriteRenderer LeftArm
        {
            get => _leftArm;
            set => _leftArm = value;
        }

        public SpriteRenderer RightArm
        {
            get => _rightArm;
            set => _rightArm = value;
        }

        public SpriteRenderer LeftLeg
        {
            get => _leftLeg;
            set => _leftLeg = value;
        }

        public SpriteRenderer RightLeg
        {
            get => _rightLeg;
            set => _rightLeg = value;
        }

        public SpriteRenderer Mouth
        {
            get => _mouth;
            set => _mouth = value;
        }

        public SpriteRenderer Hip
        {
            get => _hip;
            set => _hip = value;
        }

        public void SetComponents(Sprite head, Sprite leftEye, Sprite rightEye, Sprite leftEar, Sprite rightEar,
            Sprite leftArm, Sprite rightArm, Sprite leftLeg, Sprite rightLeg, Sprite mouth, Sprite hip,
            Material material)
        {
            _head.sprite = head;
            _leftEye.sprite = leftEye;
            _rightEye.sprite = rightEye;
            _leftEar.sprite = leftEar;
            _rightEar.sprite = rightEar;
            _leftArm.sprite = leftArm;
            _rightArm.sprite = rightArm;
            _leftLeg.sprite = leftLeg;
            _rightLeg.sprite = rightLeg;
            _mouth.sprite = mouth;
            _hip.sprite = hip;
            _material = material;

            _head.material = _material;
            _leftEye.material = _material;
            _rightEye.material = _material;
            _leftEar.material = _material;
            _rightEar.material = _material;
            _leftArm.material = _material;
            _rightArm.material = _material;
            _leftLeg.material = _material;
            _rightLeg.material = _material;
            _mouth.material = _material;
            _hip.material = _material;
        }
    }
}
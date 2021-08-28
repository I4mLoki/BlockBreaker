using UnityEngine;
using UnityEngine.UI;

namespace DataConfig
{
    public class BlockParts : MonoBehaviour
    {
        [SerializeField] private BaseBlock _baseBlock;
        [SerializeField] private  Image _head;
        [SerializeField] private  Image _leftEye;
        [SerializeField] private  Image _rightEye;
        [SerializeField] private  Image _leftEar;
        [SerializeField] private  Image _rightEar;
        [SerializeField] private  Image _mouth;
        [SerializeField] private  Image _hip;
        [SerializeField] private  Image _leftArm;
        [SerializeField] private  Image _rightArm;
        [SerializeField] private  Image _leftLeg;
        [SerializeField] private  Image _rightLeg;
        [SerializeField] private  Material _material;

        public BaseBlock BaseBlock
        {
            get => _baseBlock;
            set => _baseBlock = value;
        }

        public Image Head
        {
            get => _head;
            set => _head = value;
        }

        public Image LeftEye
        {
            get => _leftEye;
            set => _leftEye = value;
        }

        public Image RightEye
        {
            get => _rightEye;
            set => _rightEye = value;
        }

        public Image LeftEar
        {
            get => _leftEar;
            set => _leftEar = value;
        }

        public Image RightEar
        {
            get => _rightEar;
            set => _rightEar = value;
        }

        public Image LeftArm
        {
            get => _leftArm;
            set => _leftArm = value;
        }

        public Image RightArm
        {
            get => _rightArm;
            set => _rightArm = value;
        }

        public Image LeftLeg
        {
            get => _leftLeg;
            set => _leftLeg = value;
        }

        public Image RightLeg
        {
            get => _rightLeg;
            set => _rightLeg = value;
        }

        public Image Mouth
        {
            get => _mouth;
            set => _mouth = value;
        }

        public Image Hip
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
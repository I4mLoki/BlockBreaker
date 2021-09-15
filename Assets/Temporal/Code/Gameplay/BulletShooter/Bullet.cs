using UnityEngine;

namespace Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 10f;
    
        private Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
        }
    }
}
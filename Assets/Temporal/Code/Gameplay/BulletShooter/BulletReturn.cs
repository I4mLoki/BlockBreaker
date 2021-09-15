using System;
using UnityEngine;

namespace Gameplay
{
    public class BulletReturn : MonoBehaviour
    {
        private BulletShooter _bulletShooter;

        private void Awake()
        {
            _bulletShooter = FindObjectOfType<BulletShooter>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.collider.gameObject.SetActive(false);
            _bulletShooter.ReturnBullet(collision.transform.position.x);
        }
    }
}
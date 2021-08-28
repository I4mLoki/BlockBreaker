using System;
using UnityEngine;

namespace Gameplay
{
    public class BallReturn : MonoBehaviour
    {
        private BallLauncher ballLauncher;

        private void Awake()
        {
            ballLauncher = FindObjectOfType<BallLauncher>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ballLauncher.ReturnBall();
            collision.collider.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Block") return;

            // var block = collision.gameObject.GetComponent<BlockTest>();
            // TODO Damage or gameover?
            
            // collision.gameObject.SetActive(false);
            // GameplayManager.Instance.blockList.Remove(block);
            // Destroy(collision.gameObject, .5f);
        }
    }
}
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
            collision.collider.gameObject.SetActive(false);
            ballLauncher.MoveBallLauncher(collision.transform.position.x);
            ballLauncher.ReturnBall();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField]
        private Ball ballPrefab;
        
        [SerializeField]
        private GameObject ballContainer;

        private Vector3 startDragPosition;
        private Vector3 endDragPosition;
        private LaunchPreview launchPreview;
        private List<Ball> balls;
        private Camera mainCamera;
        private int ballsReady;
        private bool canShoot;

        private void Awake()
        {
            balls = new List<Ball>();
            mainCamera = Camera.main;
            launchPreview = GetComponent<LaunchPreview>();

            CreateBall();
        }

        private void Update()
        {
            if (ballsReady != balls.Count) return;

            var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

            if (Input.GetMouseButtonDown(0))
                StartDrag(worldPosition);
            else if (Input.GetMouseButton(0))
                ContinueDrag(worldPosition);
            else if (Input.GetMouseButtonUp(0) && canShoot)
                EndDrag();
        }

        private void StartDrag(Vector3 worldPosition)
        {
            launchPreview.ShowLine(true);
            startDragPosition = worldPosition;
            launchPreview.SetStartPoint(transform.position);
        }

        private void ContinueDrag(Vector3 worldPosition)
        {
            endDragPosition = worldPosition;
            var direction = startDragPosition - endDragPosition;

            if (direction.y > 0)
            {
                canShoot = false;
                launchPreview.ShowLine(false);
                return;
            }

            canShoot = true;
            launchPreview.ShowLine(true);
            launchPreview.SetEndPoint(transform.position - direction);
        }

        private void EndDrag()
        {
            launchPreview.ShowLine(false);
            StartCoroutine(LaunchBalls());
        }

        private IEnumerator LaunchBalls()
        {
            var direction = startDragPosition - endDragPosition;
            direction.Normalize();

            foreach (var ball in balls)
            {
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(true);
                ball.GetComponent<Rigidbody2D>().AddForce(-direction);

                yield return new WaitForSeconds(0.1f);
            }

            ballsReady = 0;
        }

        private void CreateBall()
        {
            var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, ballContainer.transform);
            balls.Add(ball);
            ballsReady++;
        }
    
        public void ReturnBall()
        {
            ballsReady++;
            if (ballsReady != balls.Count) return;

            GameplayManager.Instance.EnemiesTurn();
            CreateBall();
        }
    }
}
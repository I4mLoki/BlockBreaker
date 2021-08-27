using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField]
        private Ball ballPrefab;

        private Vector3 startDragPosition;
        private Vector3 endDragPosition;
        private BlockSpawner blockSpawner;
        private LaunchPreview launchPreview;
        private List<Ball> balls;
        private Camera mainCamera;
        private int ballsReady;

        private void Awake()
        {
            balls = new List<Ball>();
            mainCamera = Camera.main;
            blockSpawner = FindObjectOfType<BlockSpawner>();
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
            else if (Input.GetMouseButtonUp(0))
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
            var direction = endDragPosition - startDragPosition;
            launchPreview.SetEndPoint(transform.position - direction);
        }

        private void EndDrag()
        {
            launchPreview.ShowLine(false);
            StartCoroutine(LaunchBalls());
        }

        private IEnumerator LaunchBalls()
        {
            var direction = endDragPosition - startDragPosition;
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
            var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            balls.Add(ball);
            ballsReady++;
        }
    
        public void ReturnBall()
        {
            ballsReady++;
            if (ballsReady != balls.Count) return;
        
            // blockSpawner.SpawnRowOfBlocks();
            CreateBall();
        }
    }
}
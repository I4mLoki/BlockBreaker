using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;
        
        [SerializeField]
        private Ball ballPrefab;

        [SerializeField]
        private GameObject ballContainer;

        private Vector3 startDragPosition;
        private Vector3 endDragPosition;
        private LaunchPreview launchPreview;
        private List<Ball> balls;
        private bool canShoot;
        private bool shootInProgress;
        private int ballsAvailable;
        private bool firstBallReturned;

        private void Awake()
        {
            balls = new List<Ball>();
            launchPreview = GetComponent<LaunchPreview>();

            CreateBall();
        }

        private void Update()
        {
            if (shootInProgress || !GameplayManager.Instance.CanPlay) return;

            var worldPosition = camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

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
            shootInProgress = true;
            StartCoroutine(LaunchBalls());
        }

        private IEnumerator LaunchBalls()
        {
            var direction = startDragPosition - endDragPosition;
            direction.Normalize();

            ballsAvailable = 0;

            foreach (var ball in balls.ToList())
            {
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(true);
                ball.GetComponent<Rigidbody2D>().AddForce(-direction);

                yield return new WaitForSeconds(0.1f);
            }
        }

        private void CreateBall()
        {
            var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, ballContainer.transform);
            ball.gameObject.SetActive(false);
            
            balls.Add(ball);
            ballsAvailable = balls.Count;
        }

        public void ReturnBall()
        {
            ballsAvailable++;

            if (ballsAvailable != balls.Count) return;
            
            CreateBall();

            shootInProgress = false;
            firstBallReturned = false;
            
            GameplayManager.Instance.EnemiesTurn();
        }

        public void MoveBallLauncher(float positionX)
        {
            if (firstBallReturned) return;

            firstBallReturned = true;
            
            DOTween.Sequence()
                .Append(gameObject.transform.DOMoveX(positionX, .3f));
        }
    }
}
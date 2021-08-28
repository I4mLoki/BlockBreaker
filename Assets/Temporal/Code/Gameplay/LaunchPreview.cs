using UnityEngine;

namespace Gameplay
{
    public class LaunchPreview : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private Vector3 dragStartPoint;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetStartPoint(Vector3 worldPoint)
        {
            dragStartPoint = worldPoint;
            Debug.Log("Start point = " + dragStartPoint);

            lineRenderer.SetPosition(0, dragStartPoint);
        }

        public void SetEndPoint(Vector3 worldPoint)
        {
            var pointOffset = worldPoint - dragStartPoint;
            var endPoint = transform.position + pointOffset;
            
            Debug.Log("End point = " + endPoint);

            lineRenderer.SetPosition(1, endPoint);
        }

        public void ShowLine(bool value)
        {
            if (lineRenderer.enabled != value) lineRenderer.enabled = value;
        }
    }
}
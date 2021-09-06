using UnityEngine;
using UnityEngine.Serialization;

public class AimAssistant : MonoBehaviour
{
    private BulletShooter _bulletShooter;
    private LineRenderer _renderer;

    private float _rayLength;
    private float _aimDeadZone;
    private int _reflectionsCount;

    private void Awake()
    {
        _bulletShooter = GetComponent<BulletShooter>();
        _renderer = GetComponent<LineRenderer>();
    }

    public void Setup()
    {
        _rayLength = _bulletShooter.RayLength;
        _aimDeadZone = _bulletShooter.AimDeadZone;
    }

    public void StartAim()
    {
        StartRay(_rayLength);
    }
    
    public void StopAim()
    {
        _renderer.positionCount = 0;
    }

    private void StartRay(float length)
    {
        Vector2 startPoint = gameObject.transform.position;
        Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (endPoint - startPoint).normalized;
        var remainingRayCastLength = length;
        _reflectionsCount = 0;

        // Don't aim down
        if (direction.y < _aimDeadZone)
        {
            _bulletShooter.CanShoot = false;
            _renderer.positionCount = 0;
            return;
        }

        _bulletShooter.CanShoot = true;
        var ray = new Ray2D(startPoint, direction*remainingRayCastLength);
        var hit = Physics2D.Raycast(startPoint, direction, remainingRayCastLength);

        var drawPoint = new Vector2();

        if (hit.collider == null)
        {
            Debug.DrawRay(startPoint, direction*remainingRayCastLength);
            drawPoint = ray.GetPoint(remainingRayCastLength);
            remainingRayCastLength = length;
        }
        else
        {
            Debug.DrawLine(startPoint, hit.point);
            drawPoint = hit.point;
            remainingRayCastLength -= hit.distance;
        }

        _renderer.positionCount = 1;
        _renderer.SetPosition(0, startPoint);

        _renderer.positionCount = 2;
        _renderer.SetPosition(1, drawPoint);

        if (remainingRayCastLength <= 0 || hit.collider == null) return;

        _reflectionsCount++;
        DeflectRay(hit, direction, remainingRayCastLength);
    }

    private void DeflectRay(RaycastHit2D previousHit, Vector2 previousDirection, float length)
    {

        var startPoint = previousHit.point;
        var deflectRotation = Quaternion.FromToRotation(-previousDirection, previousHit.normal);
        var direction = (deflectRotation*previousHit.normal).normalized;
        var remainingRayCastLength = length;

        var ray = new Ray2D(startPoint, direction*remainingRayCastLength);
        var hits = Physics2D.RaycastAll(startPoint, direction, remainingRayCastLength);
        var validHit = new RaycastHit2D();
        foreach (var hit in hits)
        {
            if (hit.collider == null || hit.collider.gameObject == previousHit.collider.gameObject) continue;

            validHit = hit;
            break;
        }

        var drawPoint = new Vector2();

        if (validHit.collider == null)
        {
            Debug.DrawRay(startPoint, direction*remainingRayCastLength);
            drawPoint = ray.GetPoint(remainingRayCastLength);
            remainingRayCastLength = length;
        }
        else
        {
            Debug.DrawLine(startPoint, validHit.point);
            drawPoint = validHit.point;
            remainingRayCastLength -= validHit.distance;
        }

        _renderer.positionCount = _reflectionsCount + 2;
        _renderer.SetPosition(_reflectionsCount + 1, drawPoint);

        if (remainingRayCastLength <= 0 || validHit.collider == null) return;

        _reflectionsCount++;
        DeflectRay(validHit, direction, remainingRayCastLength);
    }
}
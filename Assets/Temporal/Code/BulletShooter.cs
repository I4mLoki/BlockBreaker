using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    private float _rayCastLength;
    private LineRenderer _renderer;
    private bool _canShoot;

    private void Awake()
    {
        _renderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartAiming();
        else if (Input.GetMouseButton(0))
            // if (Input.GetMouseButton(0) && GameplayManager.Instance.IsPlayerTurn())
            KeepAiming();
        else if (Input.GetMouseButtonUp(0))
            StopAiming();
    }

    public void Setup(float rayCastLength)
    {
        _rayCastLength = rayCastLength;
    }

    private void StartAiming()
    {
        //TODO Remove this
        _rayCastLength = 15f;
    }

    private void KeepAiming()
    {
        /*
         * First raycast
         */

        Vector2 startPoint = gameObject.transform.position;
        Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = endPoint - startPoint;

        // Don't aim down
        if (direction.y < .5f)
        {
            _canShoot = false;
            _renderer.positionCount = 0;
            return;
        }

        _canShoot = true;

        var hits = Physics2D.RaycastAll(startPoint, direction, _rayCastLength);

        var validHit = new RaycastHit2D();
        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            validHit = hit;
            break;
        }

        if (validHit.collider == null) return;

        Debug.DrawLine(startPoint, validHit.point, Color.blue);

        _renderer.positionCount = 2;
        _renderer.SetPosition(0, startPoint);
        _renderer.SetPosition(1, validHit.point);

        if (validHit.collider.tag != "Wall") return;

        /*
         * Second raycast
         */

        var startPoint2 = validHit.point;
        var deflectRotation = Quaternion.FromToRotation(-direction, validHit.normal);
        var direction2 = deflectRotation*validHit.normal;

        var hits2 = Physics2D.RaycastAll(startPoint2, direction2, _rayCastLength);

        var validHit2 = new RaycastHit2D();
        foreach (var hit in hits2)
        {
            if (hit.collider == null || hit.collider.gameObject == validHit.collider.gameObject) continue;

            validHit2 = hit;
            break;
        }

        if (validHit2.collider == null) return;

        Debug.DrawLine(startPoint2, validHit2.point, Color.blue);

        _renderer.positionCount = 3;
        _renderer.SetPosition(2, validHit2.point);
    }

    private void StopAiming()
    {
        _renderer.positionCount = 0;
        
        if (_canShoot) Shoot();
    }

    private void Shoot()
    {
    }
}
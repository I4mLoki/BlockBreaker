using UnityEngine;

public class AimAssistant : MonoBehaviour
{
    private BulletShooter _bulletShooter;
    private LineRenderer _renderer;

    private float _aimDeadZone = .5f;
    private float _rayCastLength;

    private void Awake()
    {
        _bulletShooter = GetComponent<BulletShooter>();
        _renderer = GetComponent<LineRenderer>();
    }

    public void Setup(float rayCastLength)
    {
        _rayCastLength = rayCastLength;
    }

    public void StartAim()
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
            _bulletShooter.CanShoot = false;
            _renderer.positionCount = 0;
            return;
        }

        _bulletShooter.CanShoot = true;

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

        if (validHit2.collider.tag != "Wall") return;

        /*
         * Third raycast
         */

        var startPoint3 = validHit2.point;
        var deflectRotation2 = Quaternion.FromToRotation(-direction2, validHit2.normal);
        var direction3 = deflectRotation2*validHit2.normal;

        var hit3 = Physics2D.Raycast(startPoint3, direction3, .1f);

        // var validHit3 = new RaycastHit2D();
        // foreach (var hit in hits3)
        // {
        //     if (hit.collider == null || hit.collider.gameObject == validHit2.collider.gameObject) continue;
        //
        //     validHit3 = hit;
        //     break;
        // }

        if (hit3.collider != null) Debug.DrawLine(startPoint3, hit3.point, Color.blue);
        else Debug.DrawRay(startPoint3, direction3, Color.blue);



        _renderer.positionCount = 4;
        _renderer.SetPosition(3, hit3.point);
    }

    public void StopAim()
    {
        _renderer.positionCount = 0;
    }
}
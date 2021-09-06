using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gameplay;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Transform bulletContainer;
    
    [SerializeField]
    private float bulletShooterMoveSpeed = .3f;
    
    [SerializeField]
    private int initialBullets = 1;

    public float AimDeadZone = .1f;
    public float RayLength = 10f;

    public bool AimReady { get; set; }

    private AimAssistant _aimAssistant;
    private Action _callback;
    private bool _canShoot;
    private List<Bullet> bullets;
    private int bulletsAvailable;
    private bool _firstBulletReturned;

    private void Awake()
    {
        _aimAssistant = GetComponent<AimAssistant>();
        bullets = new List<Bullet>();
    }

    private void Start()
    {
        _aimAssistant.Setup();
    }

    private void Update()
    {
        if (!_canShoot) return;

        if (Input.GetMouseButton(0))
            StartAiming();
        else if (Input.GetMouseButtonUp(0))
            StopAiming();
    }
    
    public void StartBulletShooting(Action callback)
    {
        _callback = callback;

        if (bullets.Count == 0) CreateNewBullets(initialBullets);

        _canShoot = true;
    }

    private void StartAiming()
    {
        _aimAssistant.StartAim();
    }

    private void StopAiming()
    {
        _aimAssistant.StopAim();

        if (AimReady) StartCoroutine(Shoot());
    }

    private void CreateNewBullets(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletContainer);
            bullet.gameObject.SetActive(false);

            bullets.Add(bullet);
        }

        bulletsAvailable = bullets.Count;
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;
        bulletsAvailable = 0;

        foreach (var ball in bullets.ToList())
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(_aimAssistant.ShootDirection);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ReturnBullet(float positionX)
    {
        MoveBulletShooter(positionX);
        bulletsAvailable++;

        if (bulletsAvailable != bullets.Count) return;

        _firstBulletReturned = false;
        _callback.Invoke();
    }
    
    private void MoveBulletShooter(float positionX)
    {
        if (_firstBulletReturned) return;

        _firstBulletReturned = true;
        gameObject.transform.DOMoveX(positionX, bulletShooterMoveSpeed);
    }
}
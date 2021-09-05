using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public bool CanShoot { get; set; }

    private AimAssistant _aimAssistant;
    private float _rayCastLength = 15f;

    private void Awake()
    {
        _aimAssistant = GetComponent<AimAssistant>();
    }

    private void Start()
    {
        // TODO REMOVE HERE
        _aimAssistant.Setup(_rayCastLength);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            // if (Input.GetMouseButton(0) && GameplayManager.Instance.IsPlayerTurn())
            KeepAiming();
        else if (Input.GetMouseButtonUp(0))
            StopAiming();
    }

    public void Setup(float rayCastLength)
    {
        _aimAssistant.Setup(_rayCastLength);
    }

    private void KeepAiming()
    {
        _aimAssistant.StartAimTest();
        // _aimAssistant.StartAim();
    }

    private void StopAiming()
    {
        _aimAssistant.StopAim();
        
        if (CanShoot) Shoot();
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }
}
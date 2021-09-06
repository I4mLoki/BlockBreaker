using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public float AimDeadZone = .5f;
    public float RayLength = 10f;

    public bool CanShoot { get; set; }

    private AimAssistant _aimAssistant;

    private void Awake()
    {
        _aimAssistant = GetComponent<AimAssistant>();
    }

    private void Start()
    {
        _aimAssistant.Setup();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            // if (Input.GetMouseButton(0) && GameplayManager.Instance.IsPlayerTurn())
            StartAiming();
        else if (Input.GetMouseButtonUp(0))
            StopAiming();
    }

    private void StartAiming()
    {
        _aimAssistant.StartAim();
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
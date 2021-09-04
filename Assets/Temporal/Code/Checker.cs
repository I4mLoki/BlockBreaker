using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Raycast()
    {
        var origin = transform.position;
        var direction = transform.forward;

        Debug.DrawRay(origin, direction*10f, Color.red);
        var ray = new Ray(origin, direction);
    }
}
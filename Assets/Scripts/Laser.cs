using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private LineRenderer laser;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        laser.SetPosition(0, start.position);
        laser.SetPosition(1, end.position);
    }
}

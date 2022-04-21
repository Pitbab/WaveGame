using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _toFollow;
    [SerializeField] private float _followSpeed = 3.0f;
    [SerializeField] private float _followOffset = 0.0f;
    [SerializeField] private float _camOffset = -10.0f;


    private Vector3 _targetPos;

    void Start()
    {
        _targetPos = transform.position;
    }

    void Update()
    {
        _targetPos.z = Mathf.Lerp(transform.position.z, _toFollow.position.z + _camOffset, Time.deltaTime * _followSpeed);
        _targetPos.x = Mathf.Lerp(transform.position.x, _toFollow.position.x + _followOffset, Time.deltaTime * _followSpeed);

        transform.position = _targetPos;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _toFollow;
    [SerializeField] private float _followSpeed = 3.0f;
    [SerializeField] private float _followOffset = 0.0f;
    [SerializeField] private float _camOffset = -10.0f;

    private Vector3 _targetPos;
    private quaternion topRot;
    private quaternion isoRot;
    private quaternion targetRot;
    private const float rotSpeed = 1f;



    void Start()
    {
        _targetPos = transform.position;
        topRot = transform.rotation;
        isoRot = Quaternion.Euler(60f, 0f, 0f);
    }

    void Update()
    {
        _targetPos.z = Mathf.Lerp(transform.position.z, _toFollow.position.z + _camOffset, Time.deltaTime * _followSpeed);
        _targetPos.x = Mathf.Lerp(transform.position.x, _toFollow.position.x + _followOffset, Time.deltaTime * _followSpeed);

        transform.position = _targetPos;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
    }

    public void SwitchCamAngle()
    {
        if (_camOffset != -10f)
        {
            targetRot = isoRot;
            _camOffset = -10;
        }
        else
        {
            targetRot = topRot;
            _camOffset = 0;
        }
    }
    
}

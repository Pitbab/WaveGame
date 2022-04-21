using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class PlayerData : ScriptableObject
{
    [Range(0f, 100f)]
    public float speed;

    [Range(0f, 10f)]
    public float detectionRange;

    [Range(-50f, 0f)]
    public float gravity;

    public LayerMask WhatisInteractible;
}
    
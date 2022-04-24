using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    [Range(0f, 100f)] public float fireRate;
    [Range(0f, 100f)] public float ammoSize;
    [Range(0f, 100f)] public float ammoDamage;
    [Range(0, 100)] public int magCapacity;
    [Range(0, 1000)] public int maxReserve;
    [Range(0f, 10f)] public float reloadTime;

    public GameObject ammoPrefab;
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isAvailable;

    private void Start()
    {
        if (isAvailable)
        {
            SpawnerManager.instance.availableSpawners.Add(this);
        }
    }

    public void Spawn(GameObject mob)
    {
        SpawnerManager.instance.mobPool.SpawnAMob(transform.position, Quaternion.identity);
    }
}

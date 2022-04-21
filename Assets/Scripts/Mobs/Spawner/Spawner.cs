using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject mob)
    {
        SpawnerManager.instance.mobPool.SpawnAMob(transform.position, Quaternion.identity);
    }
}

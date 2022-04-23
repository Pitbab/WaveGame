using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobPool : Pool
{
    public void SpawnAMob(Vector3 pos, Quaternion rot)
    {
        MobController mob = GetAPoolObject() as MobController;
        mob.transform.position = pos;
        mob.transform.rotation = rot;
        mob.GetComponent<NavMeshAgent>().enabled = true;
        mob.ResetMob();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : Pool
{
    public void SpawnAMob(Vector3 pos, Quaternion rot)
    {
        MobController mob = GetAPoolObject() as MobController;
        mob.transform.position = pos;
        mob.transform.rotation = rot;
        mob.ResetMob();
    }
}

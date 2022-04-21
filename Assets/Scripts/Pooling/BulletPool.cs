using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Pool
{
    public void ShootABullet(Vector3 pos, Quaternion rot)
    {
        Bullet bullet = GetAPoolObject() as Bullet;
        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
        bullet.ResetBullet();

    }
}

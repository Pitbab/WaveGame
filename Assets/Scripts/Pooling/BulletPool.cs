using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Pool
{
    public PlayerController owner { get; private set; }

    private void OnEnable()
    {
        owner = transform.parent.Find("man_soldier").GetComponent<PlayerController>();
    }

    public void ShootABullet(Vector3 pos, Quaternion rot, GunData bulletInfo)
    {
        Bullet bullet = GetAPoolObject() as Bullet;
        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
        bullet.ResetBullet(bulletInfo.ammoDamage);

    }
}

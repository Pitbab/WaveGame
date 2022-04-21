using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolItem
{
    private Rigidbody rb;
    private int mobLayer;
    private const float maxTimeActive = 2f;
    private float currentActiveTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mobLayer = LayerMask.NameToLayer("Mobs");

    }

    public void ResetBullet()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * 100, ForceMode.Impulse);
        currentActiveTime = 0;
    }

    private void Update()
    {
        currentActiveTime += Time.deltaTime;

        if (currentActiveTime > maxTimeActive)
        {
            Remove();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == mobLayer)
        {
            SpawnerManager.instance.numberAlive--;
            Remove();
            other.gameObject.GetComponent<MobController>().Remove();
            //----//Destroy(other.gameObject);
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected Vector3 startingPos;
    protected float maxTranslation;
    protected float rotationSpeed;
    
    public virtual void TakePowerUp(PlayerController actor)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TakePowerUp(other.gameObject.GetComponent<PlayerController>());
        }
    }
}

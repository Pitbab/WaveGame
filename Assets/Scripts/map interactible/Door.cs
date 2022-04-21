using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : Interactible
{
    public Vector3 detectionZone;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private NavMeshObstacle obstacle;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(meshRenderer.bounds.center, detectionZone);
    }

    public override void Interact(PlayerController actor)
    {
        base.Interact(actor);
        meshCollider.enabled = false;
        meshRenderer.enabled = false;
        obstacle.enabled = false;


    }
}

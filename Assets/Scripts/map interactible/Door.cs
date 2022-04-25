using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : Interactible
{
    [SerializeField] private int price;

    [SerializeField] private string Infos;
    [SerializeField] private List<Spawner> spawnerInRoom = new List<Spawner>();

    private PlayerLogic currentPlayer;

    public override void Interact(PlayerLogic actor)
    {
        base.Interact(actor);

        if (actor.localCash > price)
        {
            actor.SetCash(-price);
            RemoveText(actor);
            gameObject.SetActive(false);
            
            foreach (var spawner in spawnerInRoom)
            {
                if (spawner != null)
                {
                    spawner.isAvailable = true;
                    SpawnerManager.instance.availableSpawners.Add(spawner);
                }
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            currentPlayer = other.gameObject.GetComponent<PlayerLogic>();
            currentPlayer.playerUi.SetInfo(Infos);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentPlayer != null && other.gameObject == currentPlayer.gameObject)
        {
            RemoveText(currentPlayer);
        }
    }

    private void RemoveText(PlayerLogic actor)
    {
        actor.playerUi.SetInfo("");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public static GameController instance { get; private set; }

    private const float preRoundTimer = 5f;
    private int currentWave = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(instance);
    }

    private List<Transform> players = new List<Transform>();

    private void Start()
    {
        PlayerController[] p = FindObjectsOfType<PlayerController>();

        foreach (var player in p)
        {
            players.Add(player.transform);
        }

        StartCoroutine(PreRound());
    }

    public IEnumerator PreRound()
    {
        currentWave++;
        yield return new WaitForSeconds(preRoundTimer);
        SpawnerManager.instance.StartWave(currentWave);
        
    }

    public Transform[] GetPlayersPos()
    {
        return players.ToArray();
    }
    
}

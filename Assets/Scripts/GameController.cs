using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public static GameController instance { get; private set; }

    public PlayerLogic player { get; private set; }

    private const float preRoundTimer = 5f;
    private int currentWave = 0;
    public int scoreMultiplier = 1;

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

    private List<Transform> playersPos = new List<Transform>();

    private void Start()
    {
        player = FindObjectOfType<PlayerLogic>();

        StartCoroutine(PreRound());
    }

    public IEnumerator PreRound()
    {
        currentWave++;
        
        player.playerUi.OnRoundChange?.Invoke(EventID.Round, currentWave);
        
        yield return new WaitForSeconds(preRoundTimer);
        SpawnerManager.instance.StartWave(currentWave);
        
    }

}

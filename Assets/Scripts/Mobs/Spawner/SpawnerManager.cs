using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance { get; private set; }
    [SerializeField] private int PowerPercent;
    
    public MobPool mobPool {get; private set; }
    
    private List<Spawner> spawners = new List<Spawner>();
    public List<Spawner> availableSpawners = new List<Spawner>();
    [SerializeField] private List<GameObject> mobsPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> powerUpPrefabs = new List<GameObject>();
    public int numberAlive { get; private set; }
    private int currentSpawned;
    private int totalThisWave;

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
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        mobPool = GetComponent<MobPool>();
        
        Spawner[] s = FindObjectsOfType<Spawner>();
        spawners = s.ToList();
    }

    private void SpawnAtRandom()
    {
        if (totalThisWave > currentSpawned)
        {
            availableSpawners[Random.Range(0, availableSpawners.Count)].Spawn(mobsPrefabs[0]);
            currentSpawned++;
        }
    }

    public void StartWave(int level)
    {
        currentSpawned = 0;
        numberAlive = level * 100;
        totalThisWave = numberAlive;
        GameController.instance.player.playerUi.OnNumberAliveChange?.Invoke(EventID.Alive, numberAlive);

        StartCoroutine(WaveUpdate());

    }

    private IEnumerator WaveUpdate()
    {
        while (numberAlive > 0)
        {
            yield return new WaitForSeconds(0.02f);
            SpawnAtRandom();
        }

        StartCoroutine(GameController.instance.PreRound());
        Debug.Log("wave finished");
        
    }

    public void SpawnPowerUp(Vector3 pos)
    {
        if (Random.Range(0, 100 / PowerPercent) == 0)
        {
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)], pos, quaternion.identity);
        }
    }

    public void RemoveMobFromCounter()
    {
        numberAlive--;
        GameController.instance.player.playerUi.OnNumberAliveChange?.Invoke(EventID.Alive, numberAlive);
        //invoke ui alive change
    }
}

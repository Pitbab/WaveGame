using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance { get; private set; } 
    
    public MobPool mobPool {get; private set; }
    
    private List<Spawner> spawners = new List<Spawner>();
    [SerializeField] private List<GameObject> mobsPrefabs = new List<GameObject>();
    public int numberAlive;
    private int currentSpawned;
    private int totalThisWave;
    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private TMP_Text numberText;
    private float hudUpdate = 1f;
    private float timer = 0;

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

    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            timer = Time.unscaledTime + hudUpdate;
            numberText.text = "Number alive : " + numberAlive;
        }
    }

    private void SpawnAtRandom()
    {
        if (totalThisWave > currentSpawned)
        {
            spawners[Random.Range(0, spawners.Count)].Spawn(mobsPrefabs[0]);
            currentSpawned++;
        }
    }

    public void StartWave(int level)
    {
        currentSpawned = 0;
        numberAlive = level * 500;
        totalThisWave = numberAlive;
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
}

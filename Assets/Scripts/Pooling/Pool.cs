using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolItem prefab = default;
    [SerializeField, Range(0, 20)] private int defaultSize = 0;

    private List<PoolItem> actives = new List<PoolItem>();
    private List<PoolItem> inactives = new List<PoolItem>();


    private void Start()
    {
        for(int i = 0; i < defaultSize; i++)
        {
            AddToPool();
        }
    }

    private void AddToPool()
    {
        PoolItem obj = Instantiate(prefab, transform);
        obj.OnRemove(OnRemoveCallBack);
    }

    public PoolItem GetAPoolObject()
    {
        int index = inactives.Count - 1;
        if(index < 0)
        {
            AddToPool();
            index = 0;
        }

        PoolItem obj = inactives[index];
        inactives.RemoveAt(index);
        actives.Add(obj);
        obj.Activate();
        return obj;
    }

    public void OnRemoveCallBack(PoolItem obj)
    {
        actives.Remove(obj);
        inactives.Add(obj);
    }
}

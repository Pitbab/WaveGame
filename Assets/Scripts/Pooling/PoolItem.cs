using System;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public Action<PoolItem> onRemoveCallBack;

    public void OnRemove(Action<PoolItem> callback)
    {
        onRemoveCallBack = callback;
        Remove();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Remove()
    {
        onRemoveCallBack?.Invoke(this);
        gameObject.SetActive(false);
    }
}

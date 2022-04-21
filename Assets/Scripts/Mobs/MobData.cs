using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob Data")]
public class MobData : ScriptableObject
{
    [Range(0.1f, 10f)] 
    public float targetUpdateTime;
}

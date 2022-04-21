using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob Data")]
public class MobData : ScriptableObject
{
    [Range(0.1f, 10f)] 
    public float targetUpdateTime;

    [Range(1f, 1000f)] 
    public float health;

    [Range(1f, 100f)]
    public float attackDamage;

    [Range(1f, 100f)] 
    public float attackSpeed;

    [Range(0.01f, 10f)] 
    public float attackRange;

    public LayerMask whatIsPlayer;
}

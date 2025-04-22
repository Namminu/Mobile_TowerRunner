using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDataBaseSo : ScriptableObject
{
	public GameObject _obj;
}

[CreateAssetMenu]
public class MonsterDataBaseSo : EnemyDataBaseSo
{
	public int _damage;
	public int _maxHealth;
}

[CreateAssetMenu]
public class ObstacleDataBaseSo : EnemyDataBaseSo
{
	public int _damage;
}
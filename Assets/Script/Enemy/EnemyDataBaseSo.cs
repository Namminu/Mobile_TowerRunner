using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDataBaseSo : ScriptableObject
{
	public Sprite _sprite;
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
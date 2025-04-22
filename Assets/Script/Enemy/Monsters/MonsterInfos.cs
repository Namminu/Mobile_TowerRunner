using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DB/MonsterInfo")]
public class MonsterInfos : ScriptableObject
{
	public List<MonsterDataBaseSo> entries = new List<MonsterDataBaseSo>();

	private Dictionary<int, MonsterDataBaseSo> _searchInfo;
}


[CreateAssetMenu(menuName = "DB/ObstacleInfo")]
public class ObstacleInfos : ScriptableObject
{
	public List<ObstacleDataBaseSo> entries = new List<ObstacleDataBaseSo>();

	private Dictionary<int,  ObstacleDataBaseSo> _searchInfo;
}
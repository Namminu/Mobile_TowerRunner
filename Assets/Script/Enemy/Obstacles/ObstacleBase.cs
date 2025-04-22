using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour, IPoolable
{
	[SerializeField] private ObstacleDataBaseSo _db;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnDestroy()
	{
	}

	public void OnSpawn()
	{
		Initialize();
	}

	private void Initialize()
	{
		
	}
}

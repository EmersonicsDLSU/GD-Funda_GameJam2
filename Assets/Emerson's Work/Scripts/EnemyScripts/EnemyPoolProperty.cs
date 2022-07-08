using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPoolProperty
{
    //enemy Properties
    public int enemySizeLimit = 4;
    public bool isFixedAllocation = true;

    //the type of pool and the originalObj; both are required, set you're preffered values for the constructors(maxSize, isFixAllocation?)
    public GameObject enemyGO;
    [HideInInspector] public ObjPools enemyPool;
    [HideInInspector] public Transform poolableLocation;
    [HideInInspector] public string spawnName;
}

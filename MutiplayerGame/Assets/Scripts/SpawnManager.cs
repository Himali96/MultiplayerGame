using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager _instance;

    SpawnPoint[] spawnpoints;

    private void Awake()
    {
        _instance = this;
        spawnpoints = GetComponentsInChildren<SpawnPoint>();
    }

    public Transform GetSpawnpoints ()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
    }
}

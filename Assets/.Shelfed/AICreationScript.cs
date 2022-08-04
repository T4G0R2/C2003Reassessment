//AI creation Script - requires game object

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AICreationScript : MonoBehaviour
{
    // With Prefabs, drag prefab into inspector
    public GameObject EnemyPrefab;
    public GameObject HostagePrefab;
    public GameObject Sniper1Prefab;
    public GameObject Sniper2Prefab;
    public GameObject Sniper3Prefab;
    private Vector3[] SpawnLocations = { new Vector3{x = 0, y = 0, z = 0},
        new Vector3{x = 1, y = 0, z = 0},
        new Vector3{x = 2, y = 0, z = 0},
        new Vector3{x = 3, y = 0, z = 0},
        new Vector3{x = 4, y = 0, z = 0}
    };
    private GameObject Enemy;
    private GameObject Hostage;
    private GameObject Sniper1;
    private GameObject Sniper2;
    private GameObject Sniper3;

    private void SpawnEntities()
    {
        Enemy = Instantiate(EnemyPrefab, SpawnLocations[0] , Quaternion.identity);
        Enemy.AddComponent<EnemyAI>();
        Hostage = Instantiate(HostagePrefab, SpawnLocations[1], Quaternion.identity);
        Hostage.AddComponent<HostageAI>();
        Sniper1 = Instantiate(Sniper1Prefab, SpawnLocations[2], Quaternion.identity);
        Sniper1.AddComponent<SniperScript>();
        Sniper2 = Instantiate(Sniper2Prefab, SpawnLocations[3], Quaternion.identity);
        Sniper2.AddComponent<SniperScript>();
        Sniper3 = Instantiate(Sniper3Prefab, SpawnLocations[4], Quaternion.identity);
        Sniper3.AddComponent<SniperScript>();
    }

    private void Awake()
    {
        SpawnEntities();
    }
}
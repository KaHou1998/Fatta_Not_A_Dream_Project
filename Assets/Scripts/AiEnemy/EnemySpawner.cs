using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyManager enemyManager;
    private List<GameObject> enemies;

    [Header("Reference")]
    public GameObject enemyPrefab;

    [Header("Settings")]
    public float spawnTime = 3;
    public int spawnRate = 1;
    public float spawnRadius = 4.0f;
    public int maxSpawn = 5;

    float spawnCounter;

    private void Awake()
    {
        enemyManager = EnemyManager.instance;
        enemies = new List<GameObject>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            for (int i = 0; i < spawnRate; i++)
            {
                if (enemies.Count < maxSpawn)
                {
                    GameObject _enemy = Instantiate(enemyPrefab, RandomLocation(), Quaternion.identity);
                    enemyManager.Enemies.Add(_enemy);
                    enemies.Add(_enemy);
                }
            }
            spawnCounter = spawnTime;
        }    
    }

    Vector3 RandomLocation()
    {
        Vector3 SpawnLocation = new Vector3(Random.Range(this.transform.position.x-spawnRadius, this.transform.position.x+spawnRadius), 
                                            1.036f, Random.Range(this.transform.position.z - spawnRadius, this.transform.position.z + spawnRadius));
        return SpawnLocation;
    }
}

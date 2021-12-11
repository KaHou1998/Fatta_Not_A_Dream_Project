using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyManager enemyManager;
    private List<GameObject> enemies;

    [Header("Reference")]
    public GameObject enemyPrefab;
    public Waypoint waypointPrefab;

    [Header("Settings")]
    public float spawnTime = 3;
    public int spawnRate = 1;
    public float spawnRadius = 4.0f;
    public int maxSpawn = 5;

    float spawnCounter;

    private void Start()
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
                    _enemy.GetComponent<NPC_ClassBased>().patrolPoint = new List<Waypoint>();
                    /*for (int t = 0; t < waypoints.Count; t++)
                    {
                        _enemy.GetComponent<NPC_ClassBased>().patrolPoint.Add(waypoints[t]);
                    }*/
                    enemyManager.Enemies.Add(_enemy);
                    enemies.Add(_enemy);
                }
            }
            spawnCounter = spawnTime;
        }
    }

    Vector3 RandomLocation()
    {
        //Vector3 SpawnLocation = new Vector3(Random.Range(this.transform.position.x-spawnRadius, this.transform.position.x+spawnRadius), 
        //1.036f, Random.Range(this.transform.position.z - spawnRadius, this.transform.position.z + spawnRadius));

        //Vector3 SpawnLocation = new Vector3(Random.Range(transform.position.x - spawnRadius, this.transform.position.x + spawnRadius), 1.036f, Random.Range(this.transform.position.z - spawnRadius, this.transform.position.z + spawnRadius));
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        Vector3 SpawnLocation = new Vector3(randomPos.x + transform.position.x  , transform.position.y, randomPos.z + transform.position.z);
        return SpawnLocation;
    }

    /*public void GenerateRandomWaypoint()
    {
        GameObject waypoint = Instantiate(waypointPrefab.gameObject, RandomLocation(), Quaternion.identity);
        waypoint.transform.parent = this.transform;
        waypoints.Add(waypoint.GetComponent<Waypoint>());
    }

    public void RemoveLastWaypoint()
    {
        Waypoint waypoint = waypoints[waypoints.Count - 1];
        waypoints.Remove(waypoint);
        DestroyImmediate(waypoint);
    }*/

}

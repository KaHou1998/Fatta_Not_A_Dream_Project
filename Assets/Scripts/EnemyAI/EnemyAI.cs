using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : EnemyFSM
{
    public NavMeshAgent agent;
    public GameObject target;

    private void Start()
    {
        agent.speed = app.model.enemyMovementSpeed;
    }

    public void Update()
    {
        
    }
}

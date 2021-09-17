using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
public class EnemyNPC : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    FiniteStateMachine finateStateMachine;

    
    public void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        finateStateMachine = this.GetComponent<FiniteStateMachine>();
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }
}

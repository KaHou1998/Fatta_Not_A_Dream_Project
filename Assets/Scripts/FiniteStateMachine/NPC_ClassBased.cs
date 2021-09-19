using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_ClassBased : MonoBehaviour
{
    [SerializeField]
    private string currentStateName;
    private EnemyState currentState;

    public NPC_EnemyAttackState attackState = new NPC_EnemyAttackState();
    public NPC_EnemyIdleState idleState = new NPC_EnemyIdleState();

    public NavMeshAgent navAgent;

    [HideInInspector]
    public GameObject attackTarget;

    public GameObject playerRef;

    public Transform[] patrolPoint;

    public int patrolPointCount;

    public float triggerRange;

    public float meleeRange;

    private void OnEnable()
    {
        currentState = idleState;
        patrolPointCount = 0;
    }


    private void Update()
    {
        FindTarget();
        currentState = currentState.DoState(this);
    }

    void FindTarget()
    {
        /*if(playerRef == null)
        {
            Debug.LogWarning("Player no ref");
            attackTarget = null;
            return;
        }

        if (Vector3.Distance(playerRef.transform.position, transform.position) < 10.0f)
        {
            attackTarget = playerRef;
            currentState = attackState;
        }
        else
        {
            attackTarget = null;
        }*/
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}

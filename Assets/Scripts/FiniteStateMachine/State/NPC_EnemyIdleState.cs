using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_EnemyIdleState : EnemyState
{
    public EnemyState DoState(NPC_ClassBased npc)
    {
        //Debug.Log("Idle");
        npc.navAgent = npc.GetComponent<NavMeshAgent>();

        Patrol(npc);

        if (Vector3.Distance(npc.playerRef.transform.position, npc.transform.position) < npc.triggerRange)
        {
            npc.navAgent.ResetPath();
            return npc.attackState;
        }
        else
        {
            return npc.idleState;
        }
    }

    void Patrol(NPC_ClassBased npc)
    {
        if(npc.patrolPoint[npc.patrolPointCount] != null)
        {
            float dist = npc.navAgent.remainingDistance;
            if(npc.navAgent != null)
            {
                if (dist != Mathf.Infinity && npc.navAgent.pathStatus == NavMeshPathStatus.PathComplete && npc.navAgent.remainingDistance == 0)
                {
                    npc.patrolPointCount = (npc.patrolPointCount + 1) % npc.patrolPoint.Length;
                }
                npc.navAgent.SetDestination(npc.patrolPoint[npc.patrolPointCount].position);
            }
            
        }
        else
        {
            Debug.LogWarning("Patrol point empty !!!");
        }      
    }  
}

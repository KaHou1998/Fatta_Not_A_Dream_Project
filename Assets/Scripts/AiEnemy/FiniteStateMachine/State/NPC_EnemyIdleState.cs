using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_EnemyIdleState : EnemyState
{
    float DelayCounter;
    Vector3 PetrolLocation;

    public void OnEnter(NPC_ClassBased npc)
    {
        DelayCounter = npc.getpatrolDelay();
        npc.navAgent = npc.GetComponent<NavMeshAgent>();
        npc.anim = npc.GetComponentInChildren<Animator>();
    }

    public void DoState(NPC_ClassBased npc)
    {
        //Debug.Log("Idle");
       
        Patrol(npc);

        if (Vector3.Distance(npc.playerRef.transform.position, npc.transform.position) < npc.triggerRange)
        {
            npc.navAgent.ResetPath();
            //return npc.attackState;
            npc.ChangeState(npc.attackState);
        }
        else
        {
            //return npc.idleState;
        }
    }

    void Patrol(NPC_ClassBased npc)
    {

        if (npc.patrolPoint[npc.patrolPointCounter] != null)
        {
            float dist = npc.navAgent.remainingDistance;
            if (npc.navAgent != null)
            {
                if (dist != Mathf.Infinity && npc.navAgent.pathStatus == NavMeshPathStatus.PathComplete && npc.navAgent.remainingDistance == 0)
                {
                    if(CountDown())
                    {
                        npc.patrolPointCounter = (npc.patrolPointCounter + 1) % npc.patrolPoint.Length;                        
                        DelayCounter = npc.getpatrolDelay();
                        bool useable = false;
                        while(useable == false)
                        {
                            NavMeshHit hit;
                            if (NavMesh.SamplePosition(npc.patrolPoint[npc.patrolPointCounter].GenerateRandomPoint(), out hit, 1.0f, NavMesh.AllAreas))
                            {
                                PetrolLocation = hit.position;
                                useable = true;
                            }
                        }

                    }
                    npc.anim.SetBool("Moving", false);
                }
                else
                {
                    npc.anim.SetBool("Moving", true);
                }
                //npc.navAgent.SetDestination(npc.patrolPoint[npc.patrolPointCounter].GenerateRandomPoint());
                npc.navAgent.SetDestination(PetrolLocation);
            }

        }
        else
        {
            Debug.LogWarning("Patrol point empty !!!");
        }
    }

    bool CountDown()
    {
        DelayCounter -= Time.deltaTime;
        if (DelayCounter <= 0)
        {
            return true;            
        }
        return false;
    }

    public void OnExit()
    {
       
    }
}

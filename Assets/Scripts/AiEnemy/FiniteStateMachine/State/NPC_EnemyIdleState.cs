using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_EnemyIdleState : EnemyState
{
    float DelayCounter;
    Vector3 PatrolLocation;

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

        /*if (Vector3.Distance(npc.playerRef.transform.position, npc.transform.position + npc.trOffset) < npc.triggerRange)
        {
            npc.navAgent.ResetPath();
            npc.ChangeState(npc.attackState);
        }*/

        if(npc.FindVisibleTargets())
        {
            npc.navAgent.ResetPath();
            npc.ChangeState(npc.attackState);
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
                        npc.patrolPoint[npc.patrolPointCounter].ExitVector(PatrolLocation);
                        npc.patrolPointCounter = (npc.patrolPointCounter + 1) % npc.patrolPoint.Count;                        
                        DelayCounter = npc.getpatrolDelay();                       
                        PatrolLocation = npc.patrolPoint[npc.patrolPointCounter].AvailablePoint();

                    }
                    npc.anim.SetBool("Moving", false);
                }
                else
                {
                    npc.anim.SetBool("Moving", true);
                }
                //npc.navAgent.SetDestination(npc.patrolPoint[npc.patrolPointCounter].GenerateRandomPoint());
                if(PatrolLocation != Vector3.zero)
                {
                    npc.navAgent.SetDestination(PatrolLocation);
                }
                else
                {
                    npc.patrolPointCounter = (npc.patrolPointCounter + 1) % npc.patrolPoint.Count;
                    PatrolLocation = npc.patrolPoint[npc.patrolPointCounter].AvailablePoint();
                }
                    
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

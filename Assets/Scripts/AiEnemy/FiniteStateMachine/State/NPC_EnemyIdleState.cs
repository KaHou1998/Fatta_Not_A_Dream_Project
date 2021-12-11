using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_EnemyIdleState : EnemyState
{
    float DelayCounter;
    IdleState curState;

    NPC_ClassBased classBased;
    enum IdleState
    {
        move,
        generating,
        stop
    };

    public void OnEnter(NPC_ClassBased npc)
    {
        classBased = npc;
        DelayCounter = classBased.getpatrolDelay();
        classBased.navAgent = classBased.GetComponent<UnityEngine.AI.NavMeshAgent>();
        classBased.anim = classBased.GetComponentInChildren<Animator>();
        curState = IdleState.generating;
    }

    public void DoState()
    {        
        Debug.DrawRay(classBased.transform.position + new Vector3(0, 1f, 0), classBased.transform.TransformDirection(Vector3.forward) * 2f, Color.yellow);
        switch (curState)
        {
            //generate next position
            case IdleState.generating:
                float randomDistance = Random.Range(0.0f, 10.0f);
                int randomDirection = Random.Range(0, 4);
                Vector3 newPosition;
                if(randomDirection == 0)
                {
                    newPosition = new Vector3(randomDistance, 0, 0);
                }
                else if(randomDirection == 1)
                {
                    newPosition = new Vector3(-randomDistance, 0, 0);
                }
                else if (randomDirection == 2)
                {
                    newPosition = new Vector3(0, 0, randomDistance);
                }
                else if (randomDirection == 3)
                {
                    newPosition = new Vector3(0, 0, -randomDistance);
                }
                else
                {
                    newPosition = classBased.transform.position;
                }
                Vector3 targetPosition = newPosition + classBased.transform.position;
                NavMeshPath navMeshPath = new NavMeshPath();
                if(classBased.navAgent.CalculatePath(targetPosition, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    Debug.Log("Reachable");
                    classBased.navAgent.SetDestination(targetPosition);
                    curState = IdleState.move;
                }
                else
                {
                    Debug.Log("Fail");
                }                                
                break;

            //move to selected position
            case IdleState.move:
                RaycastHit rayHit;
                int random = Random.Range(0, 100);
                if(random < 30)
                {
                    if (classBased.navAgent.remainingDistance != Mathf.Infinity &&
                    classBased.navAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                    classBased.navAgent.remainingDistance == 0)
                    {
                        Debug.Log("Finish Move");
                        curState = IdleState.generating;
                    }
                    else if (Physics.Raycast(classBased.transform.position + new Vector3(0, 1f, 0), classBased.transform.TransformDirection(Vector3.forward), out rayHit, 2f))
                    {
                        curState = IdleState.generating;
                    }
                }
                else
                {
                    float randomCountdown = Random.Range(0.0f, 5.0f);
                    DelayCounter = randomCountdown;
                    curState = IdleState.stop;
                }
                
                break;

            //stop for period of time
            case IdleState.stop:              
                if (CountDown())
                {
                    curState = IdleState.generating;
                }
                Debug.Log("Stop");
                break;
        }

        if(classBased.FindVisibleTargets())
        {
            classBased.ChangeState(classBased.attackState);
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



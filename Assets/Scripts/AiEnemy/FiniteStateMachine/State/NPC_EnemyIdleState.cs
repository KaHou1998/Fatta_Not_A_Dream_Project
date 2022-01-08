using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_EnemyIdleState : EnemyState
{
    float DelayCounter;
    //IdleState curState;
    MinecraftState curState;
    Vector3 randomRotation;
    float moveDistance;
    Vector3 targetPosition;

    NPC_ClassBased classBased;
    enum IdleState
    {
        move,
        rotate,
        generating_rotation,
        generating_destination,
        stop
    };

    enum MinecraftState
    {
        idle, 
        decide,
        move,
        rotate
    };

    public void OnEnter(NPC_ClassBased npc)
    {
        classBased = npc;
        DelayCounter = classBased.getpatrolDelay();
        classBased.navAgent = classBased.GetComponent<UnityEngine.AI.NavMeshAgent>();
        classBased.anim = classBased.GetComponentInChildren<Animator>();
        curState = MinecraftState.decide;
    }

    public void DoState()
    {        
        Debug.DrawRay(classBased.transform.position + new Vector3(0, 1f, 0), classBased.transform.TransformDirection(Vector3.forward) * 2f, Color.yellow);
        MinecraftMovement();

        if (classBased.FindVisibleTargets())
        {
            classBased.ChangeState(classBased.attackState);
        }
        
    }

    void MinecraftMovement()
    {
        switch (curState)
        {
            case MinecraftState.idle:
                
                if(CountDown())
                {
                    curState = MinecraftState.decide;
                    Debug.Log("Decide");
                }
                break;          
            case MinecraftState.decide:
                
                if (Random.Range(0, 100f) > 50f)
                {                    
                    targetPosition = classBased.transform.position + Random.insideUnitSphere * 5.0f;
                    NavMeshHit hit;
                    if(NavMesh.SamplePosition (targetPosition, out hit, 1.0f, NavMesh.AllAreas))
                    {
                        curState = MinecraftState.move;
                        Debug.Log("Move");
                    }
                }
                else
                {
                    curState = MinecraftState.rotate;
                    randomRotation = new Vector3(0, Random.Range(classBased.transform.rotation.eulerAngles.y - 20.0f, classBased.transform.rotation.eulerAngles.y + 20.0f), 0);
                    Debug.Log("Rotate");
                }
                DelayCounter = Random.Range(0.5f, 2.0f);
                break;
            case MinecraftState.move:
                if (!CountDown())
                {
                    classBased.navAgent.SetDestination(targetPosition);
                }
                else
                {
                    curState = MinecraftState.idle;
                    Debug.Log("Idle");
                    DelayCounter = Random.Range(0.5f, 2.0f);
                }
                break;
            case MinecraftState.rotate:
                if (!CountDown())
                {
                    classBased.transform.eulerAngles = Vector3.Lerp(classBased.transform.rotation.eulerAngles, randomRotation, Time.deltaTime * 2.0f);
                }
                else
                {
                    curState = MinecraftState.idle;
                    Debug.Log("Idle");
                    DelayCounter = Random.Range(0.5f, 2.0f);
                }
                break;
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



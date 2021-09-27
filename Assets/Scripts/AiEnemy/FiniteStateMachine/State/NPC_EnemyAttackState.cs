using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_EnemyAttackState : EnemyState
{
    public enum attackState
    {
        searchEnemy,
        attackStart,
        attacking,
        attackEnd
    };

    public attackState _attackState;

    float attackDelayCounter;

    public void OnEnter(NPC_ClassBased npc)
    {
        npc.navAgent = npc.GetComponent<NavMeshAgent>();
        npc.mesh = npc.GetComponent<Renderer>();
        npc.anim = npc.GetComponentInChildren<Animator>();
        
    }

    public void DoState(NPC_ClassBased npc)
    {       
        if (Vector3.Distance(npc.transform.position, npc.playerRef.transform.position) > npc.triggerRange + (npc.transform.localScale.x / 2))
        {
            ResetState();
            //return npc.idleState;
            npc.ChangeState(npc.idleState);
        }

        switch (_attackState)
        {
            case attackState.searchEnemy:
                MoveToAttackTarget(npc);
                Debug.Log("search");
                break;

            case attackState.attackStart:
                _attackState = attackState.attacking;
                Debug.Log("attack start");
                break;

            case attackState.attacking:
                npc.anim.SetBool("Moving", false);
                if(CountDown())
                {
                    _attackState = attackState.attackEnd;
                    Debug.Log("Duel damage: " + npc.getDamage());
                    npc.anim.SetTrigger("IsAttack");
                    attackDelayCounter = npc.getAttackDelay();
                }
                Debug.Log("attacking");
                break;

            case attackState.attackEnd:
                _attackState = attackState.searchEnemy;
                Debug.Log("attack end");
                break;
        }

        

        if (_attackState == attackState.attackEnd)
        {
            ResetState();
            //return npc.idleState;
            npc.ChangeState(npc.idleState);
        }
        else
        {
            //return npc.attackState;
            npc.ChangeState(npc.attackState);
        }      
    }

    void MoveToAttackTarget(NPC_ClassBased npc)
    {
        if (npc.playerRef != null)
        {
              if (Vector3.Distance(npc.transform.position, npc.playerRef.transform.position) < npc.meleeRange + (npc.transform.localScale.x / 2))
              {
                   npc.navAgent.ResetPath();
                   npc.anim.SetBool("Moving", true);
                   _attackState = attackState.attackStart;
              }
              else
              {
                   npc.anim.SetBool("Moving", true);
                   npc.navAgent.SetDestination(npc.playerRef.transform.position);
              }            
        }
      
    }

    bool CountDown()
    {
        attackDelayCounter -= Time.deltaTime;
        if(attackDelayCounter <= 0)
        {                     
            return true;
        }
        return false;
    }

    void ResetState()
    {
        _attackState = attackState.searchEnemy;
    }

    public void OnExit()
    {

    }
}

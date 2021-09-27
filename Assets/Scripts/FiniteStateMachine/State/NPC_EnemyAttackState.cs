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

    float attackDelay = 2;
    float attackDelayCounter; 
    public EnemyState DoState(NPC_ClassBased npc)
    {
        npc.navAgent = npc.GetComponent<NavMeshAgent>();
        npc.mesh = npc.GetComponent<Renderer>();
        npc.anim = npc.GetComponentInChildren<Animator>();
        npc.attackAnim = npc.GetComponentInChildren<Animation>();

        if (Vector3.Distance(npc.transform.position, npc.playerRef.transform.position) > npc.triggerRange + (npc.transform.localScale.x / 2))
        {
            ResetState();
            return npc.idleState;
        }

        switch (_attackState)
        {
            case attackState.searchEnemy:
                MoveToAttackTarget(npc);
                Debug.Log("search");
                break;

            case attackState.attackStart:
                _attackState = attackState.attacking;
                attackDelayCounter = attackDelay;                
                Debug.Log("attack start");
                break;

            case attackState.attacking:
                npc.anim.SetBool("Moving", false);
                CountDown(attackState.attackEnd, npc.getDamage(), npc.attackAnim);
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
            return npc.idleState;
        }
        else
        {
            return npc.attackState;
        }      
    }

    void MoveToAttackTarget(NPC_ClassBased npc)
    {
        if (npc.playerRef != null)
        {
              if (Vector3.Distance(npc.transform.position, npc.playerRef.transform.position) < npc.meleeRange + (npc.transform.localScale.x / 2))
              {
                   npc.navAgent.ResetPath();                  
                   _attackState = attackState.attackStart;
              }
              else
              {
                   npc.navAgent.SetDestination(npc.playerRef.transform.position);
              }            
        }
      
    }

    void CountDown(attackState nextStateToChange, int damage, Animation anim)
    {
        attackDelayCounter -= Time.deltaTime;
        if(attackDelayCounter <= 0)
        {          
            Debug.Log("Duel damage: " + damage);

            _attackState = nextStateToChange;
            attackDelayCounter = attackDelay;
        }
    }

    void ResetState()
    {
        _attackState = attackState.searchEnemy;
    }
}

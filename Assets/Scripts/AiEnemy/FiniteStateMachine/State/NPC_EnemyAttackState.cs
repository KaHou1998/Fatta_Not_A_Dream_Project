using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    NPC_ClassBased classBased;


    public void OnEnter(NPC_ClassBased npc)
    {
        classBased = npc;
        classBased.mesh = classBased.GetComponent<Renderer>();
        classBased.anim = classBased.GetComponentInChildren<Animator>();

    }

    public void DoState()
    {       
        switch (_attackState)
        {
            case attackState.searchEnemy:
                MoveToAttackTarget();
                Debug.Log("search");
                break;

            case attackState.attackStart:
                _attackState = attackState.attacking;
                classBased.navAgent.isStopped = true;
                Debug.Log("attack start");
                break;

            case attackState.attacking:
                classBased.transform.LookAt(classBased.playerRef.transform);
                Attack();
                Debug.Log("attacking");
                break;

            case attackState.attackEnd:
                _attackState = attackState.searchEnemy;
                classBased.navAgent.isStopped = false;
                Debug.Log("attack end");
                break;
        }

        

        if (_attackState == attackState.attackEnd)
        {
            ResetState();
            classBased.ChangeState(classBased.idleState);
        }
        else
        {
            classBased.ChangeState(classBased.attackState);
        }  
    }

    void MoveToAttackTarget()
    {
        if (classBased.playerRef != null)
        {
            if (IsPlayerInTriggerRange())
            {
                   _attackState = attackState.attackStart;
            }
            else 
            {
                //classBased.anim.SetBool("Moving", true);
                classBased.transform.LookAt(classBased.playerRef.transform);
                classBased.navAgent.SetDestination(classBased.playerRef.transform.position);
            }            
        }
      
    }

    void Attack()
    {
        if (CountDown())
        {            
            Debug.Log("Duel damage: " + classBased.getDamage());
            classBased.anim.SetTrigger("IsAttack");
            attackDelayCounter = classBased.getAttackDelay();
            if(!IsPlayerInTriggerRange())
            {
                _attackState = attackState.attackEnd;
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

    bool IsPlayerInTriggerRange()
    {
        if(Vector3.Distance(classBased.transform.position, classBased.playerRef.transform.position) < classBased.meleeRange + (classBased.transform.localScale.x / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

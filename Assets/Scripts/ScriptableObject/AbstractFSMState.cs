using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE, 
    ACTIVE,
    COMPLETE,
    TERMINATED,
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent _navMeshAgent;
    protected EnemyNPC _npc;
    public ExecutionState ExecutionState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC = true;
        ExecutionState = ExecutionState.ACTIVE;

        //Does nav mesh agent exist?
        successNavMesh = (_navMeshAgent != null);

        //Does the executing agent exits?
        successNPC = (_npc != null);

        return successNavMesh && successNPC;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETE;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if(navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void setExecutingNPC(EnemyNPC npc)
    {
        if(npc != null)
        {
            _npc = npc;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface EnemyState 
{
    void DoState();
    void OnEnter(NPC_ClassBased npc);

    void OnExit();
}

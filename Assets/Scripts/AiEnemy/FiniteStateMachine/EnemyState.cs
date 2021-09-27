using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState 
{
    void DoState(NPC_ClassBased npc);
    void OnEnter(NPC_ClassBased npc);

    void OnExit();
}

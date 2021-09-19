using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState 
{
    EnemyState DoState(NPC_ClassBased npc);
}

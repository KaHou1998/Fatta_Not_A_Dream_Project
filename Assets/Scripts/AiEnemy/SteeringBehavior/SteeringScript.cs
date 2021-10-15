using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringScript : MonoBehaviour
{
    NPC_ClassBased npc_ClassBased;
    GameObject target;

    private void Start()
    {
        npc_ClassBased = gameObject.GetComponent<NPC_ClassBased>();

    }
}

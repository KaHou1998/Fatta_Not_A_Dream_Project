using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "States/Patreo", order = 2)]
public class PatroState : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override bool EnterState()
    {
        if(base.EnterState())
        {
            //Grab and store the patrol points.
        }
        return base.EnterState();
    }
    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}

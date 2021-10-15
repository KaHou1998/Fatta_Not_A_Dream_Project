using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : AgentBehavior
{
    public override Steering GetSteering()
    {
        Steering steer = new Steering();
        steer.linear = target.transform.position - transform.position;
        steer.linear.Normalize();
        steer.linear = steer.linear * agent.maxAccel;
        return steer;
    }
}

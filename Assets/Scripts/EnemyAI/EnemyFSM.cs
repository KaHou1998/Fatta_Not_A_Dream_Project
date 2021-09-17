using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFSM : EnemyElement
{
    private List <Action> stack;

    public void SetState (Action state)
    {
        stack.Add(state);
    }

    public void popState()
    {
        stack.RemoveAt(stack.Count - 1);
    }

    public void update()
    {
        Action currentState = getCurrentState();
        if(stack != null)
        {
            stack.Add(currentState);
        }
    }

    public Action getCurrentState()
    {
        return stack.Count > 0 ? stack[stack.Count - 1] : null;
    }
}

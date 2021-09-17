using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyElement
{
    // Controls the app workflow.
    public void TakeDamage(int DamageTaken)
    {
        app.model.enemyDamage = app.model.enemyDamage - DamageTaken;
    }

    
}

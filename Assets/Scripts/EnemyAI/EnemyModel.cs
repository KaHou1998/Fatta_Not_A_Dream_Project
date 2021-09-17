using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : EnemyElement
{
    private int EnemyHealth; 
    private int EnemyDamage; 
    private int EnemyLevel;
    private int EnemyMovementSpeed;
    //private Type EnemyType;

    [HideInInspector]
    public int enemyHealth { get { return EnemyHealth; } set { EnemyHealth = value; } }
    [HideInInspector]
    public int enemyDamage { get { return EnemyDamage; } set { EnemyDamage = value; } }
    [HideInInspector]
    public int enemyLevel { get { return EnemyLevel; } set { EnemyLevel = value; } }
    public int enemyMovementSpeed { get { return EnemyMovementSpeed; } set { EnemyMovementSpeed = value; } }
    //public Type enemyType { get { return EnemyType; }}

    public EnemyData enemyData;

    void Awake()
    {
        EnemyHealth = enemyData.health;
        EnemyDamage = enemyData.damage;
        EnemyLevel = enemyData.level;
        EnemyMovementSpeed = enemyData.movementSpeed;
        //EnemyType = enemyData.type;
    }
}

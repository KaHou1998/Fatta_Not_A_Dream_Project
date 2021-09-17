using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public EnemyData enemyData;

    protected override void Init()
    {
        health = enemyData.health;
        damage = enemyData.damage;
        movementSpeed = enemyData.movementSpeed;
        enemyType = enemyData.type;
    }

    protected override void setHealth(int newHealth)
    {
        health = newHealth;
    }

    protected override void setDamage(int newDamage)
    {
        damage = newDamage;
    }

    protected override void setSpeed(int newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public override int getHealth()
    {
        return health;
    }

    public override int getDamage()
    {
        return damage;
    }

    public override int getSpeed()
    {
        return movementSpeed;
    }

    public override void takeDamage(int damageAmount)
    {
        int newHealth = health - damageAmount;
        setHealth(newHealth);
    }

    public override void gotSlow(int slowAmount)
    {
        int newMovementSpeed = movementSpeed - slowAmount;
        setSpeed(slowAmount);
    }

    

}

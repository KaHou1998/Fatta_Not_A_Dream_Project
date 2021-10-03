using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour  
{
    protected int health;
    protected int damage;
    protected int movementSpeed;
    protected Type enemyType;
    protected float attackDelay;
    protected float patrolDelay;

    //Initialize enemy stat
    protected virtual void Init(){}

    protected virtual void setHealth(int newHealth) { }
    protected virtual void setSpeed(int newSpeed) { }
    protected virtual void setDamage(int newDamage) { }
    protected virtual void setAttackDelay(float newAttackDelay) { }

    public virtual int getHealth() { return health; }
    public virtual int getSpeed() { return movementSpeed; }
    public virtual int getDamage() { return damage; }
    public virtual float getAttackDelay() { return attackDelay; }
    public virtual float getpatrolDelay() { return patrolDelay; }

    public virtual void takeDamage(int damageAmount) { }
    public virtual void gotSlow(int slowAmount) { }

  


}


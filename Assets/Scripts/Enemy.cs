using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour  
{
    protected int health;
    protected int damage;
    protected int movementSpeed;
    protected Type enemyType;
   
    //Initialize enemy stat
    protected virtual void Init(){}

    protected virtual void setHealth(int newHealth) { }
    protected virtual void setSpeed(int newSpeed) { }
    protected virtual void setDamage(int newDamage) { }

    public virtual int getHealth() { return health; }
    public virtual int getSpeed() { return movementSpeed; }
    public virtual int getDamage() { return damage; }

    public virtual void takeDamage(int damageAmount) { }
    public virtual void gotSlow(int slowAmount) { }

  


}


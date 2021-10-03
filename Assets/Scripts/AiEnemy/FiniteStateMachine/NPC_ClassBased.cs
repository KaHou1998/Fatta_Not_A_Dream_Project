using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_ClassBased : Enemy     
{
    [SerializeField]
    private string currentStateName;
    private EnemyState currentState;
    public EnemyState newState;

    public NPC_EnemyAttackState attackState = new NPC_EnemyAttackState();
    public NPC_EnemyIdleState idleState = new NPC_EnemyIdleState();

    [Header("Component Reference")]
    public NavMeshAgent navAgent;
    public Renderer mesh;
    public GameObject playerRef;
    public Waypoint[] patrolPoint;
    public Animator anim;

    [HideInInspector]
    public GameObject attackTarget;

    [HideInInspector]
    public int patrolPointCounter;

    [Header("Enemy Settings")]
    public float triggerRange;
    public float meleeRange;
    public EnemyData enemyData;

   

    private void OnEnable()
    {
        currentState = idleState;
        newState = currentState;
        patrolPointCounter = 0;
        Init();
    }


    private void Update()
    {
        currentState.DoState(this);
        if (newState != currentState)
        {
            //currentState.OnExit();
            newState.OnEnter(this);
            currentState = newState;
        }
        
    }
  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }    

    protected override void Init()
    {
        health = enemyData.health;
        damage = enemyData.damage;
        movementSpeed = enemyData.movementSpeed;
        enemyType = enemyData.type;
        attackDelay = enemyData.attackDelay;
        patrolDelay = enemyData.patrolDelay;
        navAgent.speed = movementSpeed;
    }

    protected override void setAttackDelay(float newAttackDelay)
    {
        attackDelay = newAttackDelay;
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

    public override float getAttackDelay()
    {
        return attackDelay;
    }

    public override float getpatrolDelay()
    {
        return patrolDelay;
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

    public void ChangeState(EnemyState _newState)
    {
        newState = _newState;
    }
}
